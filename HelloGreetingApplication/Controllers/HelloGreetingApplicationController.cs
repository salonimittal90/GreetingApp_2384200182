using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Entity;
namespace HelloGreetingApplication.Controllers;
/// <summary>
/// Class providing API for HelloGreetingg
/// </summary>

[ApiController]
[Route("[controller]")]
public class HelloGreetingApplicationController : ControllerBase
{

    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    IGreetingBL _greetingBL;

    public HelloGreetingApplicationController(IGreetingBL greetingBL)
    {
        _greetingBL = greetingBL;
    }



    [HttpDelete("DeleteGreeting/{id}")]
    public IActionResult DeleteGreeting(int id)
    {
        var isDeleted = _greetingBL.DeleteGreeting(id);

        if (!isDeleted)
        {
            return NotFound(new { Success = false, Message = "Greeting not found!" });
        }
        logger.Info("Greeting Deleted Successfully");
        return Ok(new
        {
            Success = true,
            Message = "Greeting deleted successfully!"
        });
    }

    [HttpPut("UpdateGreeting")]
    public IActionResult UpdateGreeting(int id, string newMessage)
    {
        var result = _greetingBL.UpdateGreeting(id, newMessage);

        if (result == null)
        {
            return NotFound(new { Success = false, Message = "Greeting not found!" });
        }

        logger.Info("Greeting Updated Successfully");
        return Ok(new
        {
            Success = true,
            Message = "Greeting updated successfully!",
            Data = result
        });
    }

    [HttpGet("ShowGreeting")] 
        public IActionResult GetAllGreetings()
        {
            
                var result = _greetingBL.GetAllGreetings();
            if(result == null  || result.Count ==0)
            {
            return NotFound("No greetings Found");

            }
            ResponseModel<List<UserEntity>> responseModel = new ResponseModel<List<UserEntity>>();
            responseModel.Success = true;
            responseModel.Message = "Greeting fetched Successfully";
            responseModel.Data = result;
            logger.Info("Display all ddata");
            return Ok(responseModel);
        }

    [HttpGet("FindGreeting")]

    public IActionResult GetGreetingById(int id)
    {
        var result = _greetingBL.GetGreetingById(id);
        if (result == null)
        {
            return NotFound("Greeting not found.");
        }
        ResponseModel<UserEntity> responseModel = new ResponseModel<UserEntity>();
        responseModel.Success = true;
        responseModel.Message = "Greeting fetched Successfully";
        responseModel.Data = result;
        logger.Info($"Greeting fetched: {result.Message}");
        return Ok(responseModel);

    }

        [HttpPost("SaveGreeting")]

        public IActionResult SaveGreetings(string message)
        {
            var result = _greetingBL.SaveGreetings(message);
            ResponseModel<UserEntity> responseModel = new ResponseModel<UserEntity>();
            responseModel.Success = true;
            responseModel.Message = "Greeting Saved Successfully";
            responseModel.Data = result;
            logger.Info($"Greeting saved: {result.Message}");
            return Ok(responseModel);


        }

        [HttpGet("GreetingMessage")]
        public IActionResult GetGreeting(string? firstName, string? lastName)
        {
            string result = _greetingBL.GetGreeting(firstName, lastName);
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Greeting Message Executed";
            responseModel.Data = result;
            logger.Info($"Greeting fetched {result}");
            return Ok(responseModel);
        }


        /// <summary>
        /// Get method to get the Greeting message
        /// </summary>
        /// <returns>"Hello, World!</returns>

        [HttpGet]
        public IActionResult Get()
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Hello to Greeting App API Endpoint";
            responseModel.Data = "Hello World!";
            logger.Info("Get method executed");
            return Ok(responseModel);

        }

        [HttpPost]
        public IActionResult Post(RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Request received successfully";
            responseModel.Data = $"Key: {requestModel.key}, Value: {requestModel.value}";
            logger.Info("Post method executed");
            return Ok(responseModel);

        }

        /// <summary>
        /// Put method to update a resource
        /// </summary>
        /// <returns>Success message with updated data</returns>

        [HttpPut]
        public IActionResult Put(RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Data updated successfully";
            responseModel.Data = $"Updated Key: {requestModel.key}, Updated Value: {requestModel.value}";
            logger.Info("Post method executed");
            return Ok(responseModel);
        }


        [HttpPatch]
        public IActionResult Patch(RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Data patched successfully";
            responseModel.Data = $"Patched Key: {requestModel.key}, Patched Value: {requestModel.value}";
            logger.Info("Patch method executed");
            return Ok(responseModel);
        }

        [HttpDelete]
        public IActionResult Delete(RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Data deleted successfully";
            responseModel.Data = $"Deleted Key: {requestModel.key}";
            logger.Info("Deleted method executed");
            return Ok(responseModel);
        }


    }

