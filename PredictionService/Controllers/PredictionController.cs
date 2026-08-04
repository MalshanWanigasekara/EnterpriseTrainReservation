using Microsoft.AspNetCore.Mvc;
using PredictionService.Interfaces;

namespace PredictionService.Controllers
{
    [ApiController]
    [Route("api/predictions")]
    public class PredictionController : ControllerBase
    {
        private readonly IPredictionService predictionService;

        public PredictionController( IPredictionService predictionService)
        {
            this.predictionService = predictionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrediction( int trainId,  DateTime travelDate)
        {
            var prediction = await predictionService.PredictAsync(trainId, travelDate);
            return Ok(prediction);
        }
    }
}