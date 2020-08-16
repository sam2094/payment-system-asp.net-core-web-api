using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Enums.ErrorEnums;
using Common.Resources;
using DataAccess.UnitofWork;
using Models.Entities;
using Models.Parameters.AuthParams;
using SecureLib;

namespace Services.Services.AuthServices
{
    public class AuthService : AbstractService<bool>, IAuthService
    {
        public AuthService(IUnitOfWork UoW, bool beginTransaction = false) :
              base(UoW, beginTransaction)
        {

        }

        public async Task<ContainerResult<bool>> AuthorizeAsync(PaymentAuthParam input)
        {

            string rawData = input.MakeSignatureRaw();

            Application application = await _uow.GetRepository<Application>().GetAsync(x => x.Id == input.AppId);

            if (application == null || application?.State==false)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.APPLICATION_DOES_NOT_EXISTS,
                    ErrorMessage = Resource.ORGANIZATION_DOES_NOT_EXIST,
                    StatusCode = ErrorHttpStatus.NOT_FOUND
                }); ;
                return Result;
            }

            //next update reduce database  request on each operation 
            MerchantSetting merchantSetting = await _uow.GetRepository<MerchantSetting>().GetAsync(x => x.Id == input.AppId);

            if (merchantSetting == null)
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.MERCHANT_SETTING_DOES_NOT_EXISTS,
                    ErrorMessage = Resource.MERCHANT_SETTING_DOES_NOT_EXISTS,
                    StatusCode = ErrorHttpStatus.NOT_FOUND
                }); ;
                return Result;
            }

            if (application.DependIp!= null )
            {
                List<string> ips = application.DependIp.Split(';').ToList() ;
                 if (!ips.Contains(input.Ip))
                {
                    Result.ErrorList.Add(new Error
                    {
                        ErrorCode = ErrorCodes.MERCHANT_SETTING_DOES_NOT_EXISTS,
                        ErrorMessage = Resource.MERCHANT_SETTING_DOES_NOT_EXISTS,
                        StatusCode = ErrorHttpStatus.NOT_FOUND
                    }); ;
                    return Result;
                }
            }

            string encKey = merchantSetting.EncKey;
            byte[] keybytes = Encoding.UTF8.GetBytes(encKey);
            byte[] inputassArray = Encoding.UTF8.GetBytes( input.MakeSignatureRaw());

            HmacFuncSHA256 func = new HmacFuncSHA256();

             string  signtaureres =  func.Encrypt(keybytes, inputassArray).ToHexString(); 

            if (!signtaureres.Equals(input.Signature))
            {
                Result.ErrorList.Add(new Error
                {
                    ErrorCode = ErrorCodes.SIGNATURE_IS_NOT_VALID,
                    ErrorMessage = Resource.SIGNATURE_IS_NOT_VALID,
                    StatusCode = ErrorHttpStatus.FORBIDDEN
                }); ;
                return Result;
            }

            Result.Output = true; 
            return Result;
        }
    }
}
