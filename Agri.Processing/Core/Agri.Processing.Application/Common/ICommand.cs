using Agri.Shared.Result;
using MediatR;

namespace Agri.Processing.Application.Common;

public interface ICommand : IRequest<Result>;
public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
