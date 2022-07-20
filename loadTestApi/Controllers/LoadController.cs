using loadTestApi.Interface;
using loadTestApi.Model;
using Microsoft.AspNetCore.Mvc;


namespace loadTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadController : Controller
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public LoadController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        [Route("GetAllRecords")]
        public async Task<IEnumerable<LoadData>> GetAllRecords()
        {
            return await _dataAccessProvider.GetAllRecords();
        }

        [Route("GetRecordByKey/{key}")]
        public async Task<IEnumerable<string>> GetRecordByKey(string key)
        {
            return await _dataAccessProvider.GetRecordByKey(key);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LoadData loadData)
        {
            if (ModelState.IsValid)
            {
                await _dataAccessProvider.InsertRecord(loadData);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] LoadData loadData)
        {
            if (ModelState.IsValid)
            {
                await _dataAccessProvider.UpdateRecord(loadData);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{key}")]
        public IActionResult DeleteConfirmed(string key)
        {
            var data = _dataAccessProvider.GetRecordByKey(key);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteRecord(key);
            return Ok();
        }
    }
}

