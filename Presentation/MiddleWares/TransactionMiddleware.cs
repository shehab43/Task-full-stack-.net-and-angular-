using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Presentation.MiddleWares
{
    /// <summary>
    /// Middleware to handle transactions automatically for each request.
    /// Ensures a database transaction is started and committed or rolled back appropriately.
    /// </summary>
    public class TransactionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TransactionMiddleware> _logger;

        public TransactionMiddleware(RequestDelegate next, ILogger<TransactionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Middleware execution method. Wraps the request inside a database transaction.
        /// </summary>
        /// <param name="httpContext">The HTTP context of the request.</param>
        public async Task InvokeAsync(HttpContext httpContext, Context dbContext)
        {
            var transaction = await dbContext.Database.BeginTransactionAsync(); // Remove 'using'
            try
            {
                _logger.LogInformation("Transaction started.");

                // Proceed with the request
                await _next(httpContext);

                // Commit transaction if the request completes successfully

                await transaction.CommitAsync();
                _logger.LogInformation("Transaction committed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Transaction rollback due to an exception.");

                // Rollback transaction in case of an error
                await transaction.RollbackAsync();
                throw; 
                // Rethrow the exception to allow error handling middleware to process it
            }
            finally
            {
                await transaction.DisposeAsync(); // Dispose transaction explicitly at the end
            }
        }

     
    }
}
