using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiledTest.DTO;
using FiledTest.Services.ServiceImplementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FiledTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentRequestService _paymentRequestService;

        public PaymentController(IPaymentRequestService paymentRequestService, ILogger<PaymentController> logger)
        {
            _logger = logger;
            _paymentRequestService = paymentRequestService;
        }
        [HttpGet]

        public IActionResult Get()
        {
            return Ok("welcome to the E-payment");
        }

        /// <summary>
        /// this is action method which we call to save the payment
        /// </summary>
        /// <param name="paymentRequest">Payment Request DTO </param>
        /// <returns>Response</returns>
        [HttpPost("processPayment")]
        
        public async Task<IActionResult> ProcessPayment(PaymentRequestDTO paymentRequest) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //in this the processing the payment take place
                    var paymentState = await _paymentRequestService.ProcessingPayment(paymentRequest);
                    var paymentResponse = new PaymentResponseDTO()
                    {
                        IsProcessed = paymentState.PaymentState == PaymentStateEnum.Processed
                        ,
                        PaymentState = paymentState
                    };
                    //this will check if the payment is processed or not

                    if (!paymentResponse.IsProcessed)
                        return StatusCode(500, new { error = "Payment could not be processed" });
                    return Ok(paymentResponse);
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

    }
}
