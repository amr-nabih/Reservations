using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task.Domain.Models;
using Task.Infrastructure.Entities;
using Task.Infrastructure.UnitOfWork;

namespace Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        #region Property

        private readonly ILogger<TripController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Ctor

        public TripController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<TripController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        #region Trips

        /// <summary>
        /// GetTrips
        ///  GET: api/Trip
        /// </summary>
        /// <returns>Response</returns>
        [HttpGet]
        public IActionResult GetTrips()
        {
            try
            {
                var Trips = _unitOfWork.TripRepository.Get();
                return Ok(new Response { Status = "Success", MessageEn = "Get Trips Success", MessageAr = "كل الرحلات", Errors = null, Data = Trips });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Getting Trips. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Getting Trips", MessageAr = "حدث خطا اثناء الحصول علي الرحلات", Errors = ex.Message, Data = null });
            }

        }
        /// <summary>
        /// GetTrip
        ///  GET: api/Trip/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Response</returns>
        [HttpGet("{id}")]
        public IActionResult GetTrip(int id)
        {
            try
            {
                var Trip = _unitOfWork.TripRepository.GetById(id);
                return Ok(new Response { Status = "Success", MessageEn = "Get Trip Success", MessageAr = "كل الرحلات", Errors = null, Data = Trip });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Getting Trip. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Getting Trip", MessageAr = "حدث خطا اثناء الحصول علي الرحلات", Errors = ex.Message, Data = null });
            }
        }
        /// <summary>
        /// Add
        ///  POST: api/Trip
        /// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// </summary>
        /// <param name="TripModel"></param>
        /// <returns>Response</returns>
        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] TripModel TripModel)
        {
            try
            {
                var Trip = _mapper.Map<Trip>(TripModel);
               _unitOfWork.TripRepository.Insert(Trip);
                _unitOfWork.Commit();
                return Ok(new Response { Status = "Success", MessageEn = "Insert Trip Success", MessageAr = "تمت عملية اضافة رحلة بنجاح", Errors = null, Data = Trip });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Inserting Trip. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Inserting Trip", MessageAr = "حدث خطا اثناء ادخال الرحلة", Errors = ex.Message, Data = null });
            }

        }
        /// <summary>
        /// Update
        /// Put:api/Trip/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TripModel"></param>
        /// <returns>Response</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TripModel TripModel)
        {
            try
            {
                    var entityToUpdate = _mapper.Map<Trip>(TripModel);
                entityToUpdate.Id = id;
                _unitOfWork.TripRepository.Update(entityToUpdate);
                _unitOfWork.Commit();
                return Ok(new Response { Status = "Success", MessageEn = "Update Trip Success", MessageAr = "تمت عملية نعديل رحلة بنجاح", Errors = null, Data = entityToUpdate });
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Updating Trip. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Updating Trip", MessageAr = "حدث خطا اثناء تعديل الرحلة", Errors = ex.Message, Data = null });
            }
        }
        /// <summary>
        /// Delete
        ///   DELETE: api/Trip/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
               
                    _unitOfWork.TripRepository.Delete(id);
                    _unitOfWork.Commit();
                    return Ok(new Response { Status = "Success", MessageEn = "Delete Trip Success", MessageAr = "تمت عملية حذف رحلة بنجاح", Errors = null, Data = id });
               
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Trip Status. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Trip Status", MessageAr = "حدث خطا اثناء حذف الرحلة", Errors = ex.Message, Data = null });
            }
        }
        /// <summary>
        /// TripExists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool TripExists(int id)
        {
            if( _unitOfWork.TripRepository.GetById(id)!=null)
            {
                return true;
            }
            else { return true; }
        }

        #endregion

    }
}
