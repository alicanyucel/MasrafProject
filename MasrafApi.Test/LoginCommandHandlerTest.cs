using MasrafProject.Application.Features.Auth.Login;
using MasrafProject.Application.Services;
using MasrafProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

public class LoginCommandHandlerTests
{
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly Mock<SignInManager<AppUser>> _signInManagerMock;
    private readonly Mock<IJwtProvider> _jwtProviderMock;
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        var userStoreMock = new Mock<IUserStore<AppUser>>();
        _userManagerMock = new Mock<UserManager<AppUser>>(
            userStoreMock.Object,
            null!,
            null!,
            null!,
            null!,
            null!,
            null!,
            null!,
            null!
        );

        var contextAccessor = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
        var userClaimsPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<AppUser>>();

        _signInManagerMock = new Mock<SignInManager<AppUser>>(
            _userManagerMock.Object,
            contextAccessor.Object,
            userClaimsPrincipalFactory.Object,
            null!,
            null!,
            null!,
            null!
        );

        _jwtProviderMock = new Mock<IJwtProvider>();

        _handler = new LoginCommandHandler(
            _userManagerMock.Object,
            _signInManagerMock.Object,
            _jwtProviderMock.Object
        );
    }

    [Fact]
    public async Task Handle_UserNotFound_ReturnsError()
    {
        _userManagerMock.Setup(x => x.Users)
            .Returns(new TestAsyncEnumerable<AppUser>(new List<AppUser>()));

        var command = new LoginCommand("test@example.com", "password");

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccessful);
        Assert.Equal(500, result.StatusCode);
    }

    [Fact]
    public async Task Handle_IncorrectPassword_ReturnsError()
    {
        var user = new AppUser { UserName = "testuser", Email = "test@example.com" };

        _userManagerMock.Setup(x => x.Users)
            .Returns(new TestAsyncEnumerable<AppUser>(new List<AppUser> { user }));

        _signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, "wrongpassword", true))
            .ReturnsAsync(SignInResult.Failed);

        var command = new LoginCommand("test@example.com", "wrongpassword");

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccessful);
        Assert.Equal(500, result.StatusCode);
    }

    [Fact]
    public async Task Handle_UserLockedOut_ReturnsError()
    {
        var user = new AppUser
        {
            UserName = "testuser",
            Email = "test@example.com",
            LockoutEnd = DateTime.UtcNow.AddMinutes(3)
        };

        _userManagerMock.Setup(x => x.Users)
            .Returns(new TestAsyncEnumerable<AppUser>(new List<AppUser> { user }));

        _signInManagerMock.Setup(x => x.CheckPasswordSignInAsync(user, "password", true))
            .ReturnsAsync(SignInResult.LockedOut);

        var command = new LoginCommand("test@example.com", "password");

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.False(result.IsSuccessful);
        Assert.Equal(500, result.StatusCode);
        if (result.ErrorMessages != null)
        {
            var joined = string.Join(", ", result.ErrorMessages);
            Assert.Contains("bloke", joined);
        }
    }
    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;
        public TestAsyncQueryProvider(IQueryProvider inner) { _inner = inner; }
        public IQueryable CreateQuery(Expression expression) => new TestAsyncEnumerable<TEntity>(expression);
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new TestAsyncEnumerable<TElement>(expression);
        public object Execute(Expression expression) => _inner.Execute(expression);
        public TResult Execute<TResult>(Expression expression) => _inner.Execute<TResult>(expression);
        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            
            return Execute<TResult>(expression);
        }
        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken, bool _)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
        public TestAsyncEnumerable(Expression expression) : base(expression) { }
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }
        IQueryProvider IQueryable.Provider => (IQueryProvider)new TestAsyncQueryProvider<T>(this);
    }

    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;
        public TestAsyncEnumerator(IEnumerator<T> inner) { _inner = inner; }
        public T Current => _inner.Current;
        public ValueTask DisposeAsync() { _inner.Dispose(); return ValueTask.CompletedTask; }
        public ValueTask<bool> MoveNextAsync() => new ValueTask<bool>(_inner.MoveNext());
    }
}