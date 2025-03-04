using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;

namespace HelloGreetingApplication.Controllers;
/// <summary>
/// Class providing API for HelloGreetingg
/// </summary>

[ApiController]
[Route("[controller]")]
public class HelloGreetingApplicationController : ControllerBase
{
    // create the instance of logger and return the current class logger
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    // create the reference of Interface
    private readonly IGreetingBL _greetingBL;

    public HelloGreetingApplicationController(IGreetingBL greetingBL)
    {

        _greetingBL = greetingBL;
       
    }

    [HttpGet("custom")]
    public IActionResult GetGreeting(string? firstName, string? lastName)
    {
        string result = _greetingBL.GetGreeting(firstName, lastName);
        ResponseModel<string> responseModel = new ResponseModel<string>();
        responseModel.Success = true;
        responseModel.Message = "Greeting fetched sucessfully";
        responseModel.Data = result;
        logger.Info("Greeting fetched " + result);
        return Ok(responseModel);
    }



   
    /// <summary>
    /// Get method to get the  message
    /// </summary>
    /// <returns>"Hello, World!</returns>

    [HttpGet]
    public IActionResult Get()
    {

        ResponseModel<string> responseModel = new ResponseModel<string>();
        responseModel.Success = true;
        responseModel.Message = "Hello to Greeting App API Endpoint";
        responseModel.Data = "Hello World!";
        logger.Info("Get method called successfully!");
        return Ok(responseModel);

    }

    [HttpPost]
    public IActionResult Post(RequestModel requestModel)
    {
        ResponseModel<string> responseModel = new ResponseModel<string>();
        responseModel.Success = true;
        responseModel.Message = "Request received successfully";
        responseModel.Data = $"Key: {requestModel.key}, Value: {requestModel.value}";
        logger.Info("Post method called successfully!");
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
        logger.Info("Put method called successfully!");
        return Ok(responseModel);
    }


    [HttpPatch]
    public IActionResult Patch(RequestModel requestModel)
    {
        ResponseModel<string> responseModel = new ResponseModel<string>();
        responseModel.Success = true;
        responseModel.Message = "Data patched successfully";
        responseModel.Data = $"Patched Key: {requestModel.key}, Patched Value: {requestModel.value}";
        logger.Info("Patch method called successfully!");
        return Ok(responseModel);
    }

    [HttpDelete]
    public IActionResult Delete(RequestModel requestModel)
    {
        ResponseModel<string> responseModel = new ResponseModel<string>();
        responseModel.Success = true;
        responseModel.Message = "Data deleted successfully";
        responseModel.Data = $"Deleted Key: {requestModel.key}";
        logger.Info("Delete method called successfully!");
        return Ok(responseModel);
    }



}