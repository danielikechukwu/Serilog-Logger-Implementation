using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SerilogLoggerImplementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        // Inject the logger for this controller
        private readonly ILogger<TestsController> _logger;

        // Constructor injection of ILogger<TestController>
        public TestsController(ILogger<TestsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("all-logs")]
        public IActionResult LogAllLevels()
        {
            // Log a Trace-level message
            _logger.LogTrace("LogTrace: Entering the LogAllLevels endpoint.");

            // Simulate a calculation and log it
            int calculation = 5 * 10;
            _logger.LogTrace("LogTrace: Calculation value is {Calculation}", calculation);

            // Log a Debug-level message with additional context
            _logger.LogDebug("LogDebug: Initializing debug-level logs for debugging purposes.");

            var debugInfo = new { Action = "LogAllLevels", Status = "Debugging" };

            _logger.LogDebug("LogDebug: Debug information: {@DebugInfo}", debugInfo);

            // Log an Information-level message
            _logger.LogInformation("LogInformation: XXXX - The LogAllLevels endpoint was reached successfully.");

            // Log a Warning if a certain condition is met
            bool resourceLimitApproaching = true;

            if (resourceLimitApproaching)
            {
                _logger.LogWarning("LogWarning: MMMM - Resource usage is nearing the limit.");
            }

            try
            {
                // Simulate an error scenario
                int x = 0;
                int result = 10 / x;
            }
            catch (Exception ex)
            {
                // Log an Error-level message with exception details
                _logger.LogError(ex, "LogError: An error occurred while processing the request.");
            }

            // Log a Critical-level message if a critical failure is detected
            bool criticalFailure = true;

            if (criticalFailure)
            {
                _logger.LogCritical("LogCritical: A critical system failure has been detected.");
            }

            return Ok("All logging levels demonstrated in this endpoint.");

        }
    }
}
