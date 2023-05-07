using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PairSoftAPI.Models;
using PairSoftAPI.ServiceRepository;

namespace PairSoftAPI.Migrations
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoListRepository _services;
        public ToDoController(IToDoListRepository services)
        {
            _services = services;
        }
        [HttpGet("GetToDoList")]
        public async Task<ActionResult> GetToDoList()
        {
            try
            {
                return Ok(await _services.GetToDoList());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpGet("GetListById")]
        public async Task<ActionResult> GetListById(int id)
        {
            try
            {
                var result = await _services.FindById(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [HttpDelete("DeleteList")]
        public async Task<ActionResult> DeleteList(int id)
        {
            try
            {
                var ListToDelete = await _services.DeleteList(id);

                if (ListToDelete == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                return Ok(ListToDelete);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
        [HttpPost("InsertList")]
        public async Task<ActionResult> InsertList(ToDoList toDoList)
        {
            try
            {
                if (toDoList == null)
                    return BadRequest();

                var createdlist = await _services.InsertList(toDoList);
                return Ok(createdlist);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new List record");
            }
        }
        [HttpPut("UpdateList")]
        public async Task<ActionResult> UpdateList(ToDoList toDoList)
        {
            try
            {
                if (toDoList.Id <= 0)
                    return BadRequest("List ID mismatch");

                var ListToUpdate = await _services.UpdateList(toDoList);

                if (ListToUpdate == null)
                    return NotFound();

                return Ok(ListToUpdate);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

    }
}
