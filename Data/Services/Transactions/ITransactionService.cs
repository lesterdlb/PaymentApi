using Contracts.Requests;
using ErrorOr;

namespace Data.Services.Transactions;

public interface ITransactionService
{
    Task<ErrorOr<TransactionResponse>> Add(TransactionRequest addTransactionRequest);

}