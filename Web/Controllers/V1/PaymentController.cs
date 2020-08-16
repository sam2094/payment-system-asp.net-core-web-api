using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Services.Services.PaymentService;
using Models.Parameters.Payments;
using Web.Filters;
using Common.Enums.DatabaseEnums;
using System;
using System.Linq;
using System.Security.Claims;
using Services.Services.PaymentService.Consolidate;
using Services.Services.AuthServices;

namespace Web.Controllers.V1
{
    public class PaymentController : BaseV1Controller
    {
        private readonly IAuthService _authservice; 
        private readonly IPaymentService<PaymentOutput> _paymentService;
        private readonly IPaymentStatusService<PaymentOutput> _paymentStatusService;
        private readonly IVerifyTokenService<VerifyTokenOutput> _verifyTokenService;
        private readonly IConsolidateService<ConsolidateOutput> _consolidateService;
        private readonly ICancellPaymentService<PaymentOutput> _cancellPaymentService;
        private readonly IGetBalanceService<GetBalanceOutput> _getBalanceService;
        ///// <summary>
        /////		
        ///// </summary>
        ///// <param name=_enumService"></param>
        public PaymentController(IPaymentService<PaymentOutput> paymentService,
            IVerifyTokenService<VerifyTokenOutput> verifyTokenService,
            IPaymentStatusService<PaymentOutput> paymentStatusService,
            IConsolidateService<ConsolidateOutput> consolidateService,
            ICancellPaymentService<PaymentOutput> cancellPaymentService,
            IGetBalanceService<GetBalanceOutput> getBalanceService , 
            IAuthService authservice 

            )
        {
            _paymentService = paymentService;
            _verifyTokenService = verifyTokenService;
            _paymentStatusService = paymentStatusService;
            _consolidateService = consolidateService;
            _cancellPaymentService = cancellPaymentService;
            _getBalanceService = getBalanceService;
            _authservice = authservice; 
        }

        /// <summary>
        ///		
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ClaimAuthorize(Claims.CAN_VERIFY_TOKEN)]
        [Route("verifyToken")]
        public async Task<IActionResult> VerifyToken(VerifyTokenInput input)
        {
            return Result(await _verifyTokenService.VerifyTokenAsync(input.Token));
        }

        /// <summary>
        ///		
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ClaimAuthorize(Claims.CAN_VERIFY_TOKEN)] // todo
        [Route("payment")]
        public async Task<IActionResult> Payment(PaymentInput input)
        {
            return Result(await _paymentService.PaymentAsync(input, Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value)));
        }

        /// <summary>
        ///		
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ClaimAuthorize(Claims.CAN_VERIFY_TOKEN)] // todo
        [Route("cancelPayment")]
        public async Task<IActionResult> CancellPayment(CancellPaymentInput input)
        {
            return Result(await _cancellPaymentService.CancellPaymentAsync(input, Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value)));
        }


        /// <summary>
        ///		
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ClaimAuthorize(Claims.CAN_VERIFY_TOKEN)] // todo
        [Route("paymentStatus")]
        public async Task<IActionResult> PaymentStatus(PaymentStatusInput input)
        {
            return Result(await _paymentStatusService.GetPaymentStatusAsync(input, Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value)));
        }


        /// <summary>
        ///		
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ClaimAuthorize(Claims.CAN_VERIFY_TOKEN)] //todo
        [Route("consolidate")]
        public async Task<IActionResult> Consolidate(ConsolidateInput input)
        {
            return Result(await _consolidateService.ConsolidateAsync(input, Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value)));
        }

        /// <summary>
        ///		
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PaymentAuthFilter] //todo
        [Route("getBalance")]
        public async Task<IActionResult> GetBalance()
        {
            return Result(await _getBalanceService.GetBalance());
        }
    }
}

