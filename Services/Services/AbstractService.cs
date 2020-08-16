using Common;
using Common.Enums.ErrorEnums;
using Common.Resources;
using DataAccess.UnitofWork;
using log4net;
using System;
using System.Reflection;
using System.Threading.Tasks;


namespace Services.Services
{
    public abstract class AbstractService<TOutput>
         where TOutput : new()
    {
        protected static readonly ILog _logger;
        protected readonly IUnitOfWork _uow;
        protected readonly bool _beginTransaction;

        /// <summary>
        /// Output of the logic
        /// </summary>
        public ContainerResult<TOutput> Result;

        public AbstractService(IUnitOfWork uow, bool beginTransaction = false)
        {
            _uow = uow;
            _beginTransaction = beginTransaction;
            Result = new ContainerResult<TOutput>
            {
                Output = new TOutput()
            };
        }

        static AbstractService()
        {
            _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// If the instance belong first called logic in logic-chain it will begin transaction
        /// </summary>
        /// <param name="beginTransaction"></param>
        protected void Begin(bool beginTransaction)
        {
            if (beginTransaction)
                _uow.Begin();
        }

        /// <summary>
        /// If the instance belong first called logic in logic-chain it will begin transaction
        /// </summary>
        /// <param name="beginTransaction"></param>
        protected async Task BeginAsync(bool beginTransaction)
        {
            if (beginTransaction)
                await _uow.BeginAsync();
        }

        /// <summary>
        /// If the instance belong first called logic in logic-chain it will commit transaction
        /// </summary>
        /// <param name="beginTransaction"></param>
        /// <summary>
        /// If the instance belong first called logic in logic-chain it will commit transaction
        /// </summary>
        /// <param name="beginTransaction"></param>
        protected bool Commit(bool beginTransaction)
        {
            bool result = false;
            try
            {
                if (beginTransaction) _uow.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INTERNAL_ERROR,
                    ErrorMessage = Resource.UNHANDLED_EXCEPTION,
                    StatusCode = ErrorHttpStatus.INTERNAL
                });

                _logger.Error($"Error occured commit : {ex.ToString()}");
            }
            finally
            {
                _uow.Dispose();
            }
            return result;
        }

        /// <summary>
        /// If the instance belong first called logic in logic-chain it will commit transaction
        /// </summary>
        /// <param name="beginTransaction"></param>
        protected async Task<bool> CommitAsync(bool beginTransaction)
        {
            bool result = false;
            try
            {
                if (beginTransaction) await _uow.CommitAsync();
                result = true;

            }
            catch (Exception ex)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INTERNAL_ERROR,
                    ErrorMessage = Resource.UNHANDLED_EXCEPTION,
                    StatusCode = ErrorHttpStatus.INTERNAL
                });

                _logger.Error($"Error occured commit : {ex.ToString()}");
            }
            finally
            {
                _uow.Dispose();
            }
            return result;
        }

        /// <summary>
        /// If the instance belong first called logic in logic-chain it will rollback transaction
        /// </summary>
        /// <param name="beginTransaction"></param>
        protected bool RollBack(bool beginTransaction)
        {
            bool result = false;

            try
            {
                if (beginTransaction) _uow.Rollback();
                result = true;
            }
            catch (Exception ex)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INTERNAL_ERROR,
                    ErrorMessage = Resource.UNHANDLED_EXCEPTION,
                    StatusCode = ErrorHttpStatus.INTERNAL
                });

                _logger.Error($"Error occured rollback : {ex.ToString()}");
            }
            finally
            {
                _uow.Dispose();
            }
            return result;
        }

        /// <summary>
        /// If the instance belong first called logic in logic-chain it will rollback transaction
        /// </summary>
        /// <param name="beginTransaction"></param>
        protected async Task<bool> RollBackAsync(bool beginTransaction)
        {
            bool result = false;

            try
            {
                if (beginTransaction) await _uow.RollbackAsync();
                result = true;
            }
            catch (Exception ex)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.INTERNAL_ERROR,
                    ErrorMessage = Resource.UNHANDLED_EXCEPTION,
                    StatusCode = ErrorHttpStatus.INTERNAL
                });

                _logger.Error($"Error occured rollback : {ex.ToString()}");
            }
            finally
            {
                _uow.Dispose();
            }
            return result;
        }
    }
}
