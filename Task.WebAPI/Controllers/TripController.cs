using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task.Domain.Models;
using Task.Infrastructure.Entities;
using Task.Infrastructure.UnitOfWork;

namespace Task.WebAPI.Controllers
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

        #region Categories

        /// <summary>
        /// GetCategories
        ///  GET: api/Trip
        /// </summary>
        /// <returns>Response</returns>
        [HttpGet]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = _unitOfWork.TripRepository.Get();
                return Ok(new Response { Status = "Success", MessageEn = "Get Categories Success", MessageAr = "كل الاصناف", Errors = null, Data = categories });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Getting Categories. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Getting Categories", MessageAr = "حدث خطا اثناء الحصول علي الاصناف", Errors = ex.Message, Data = null });
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
            /// remove breakpoint from here 
            try
            {
                var Trip = _unitOfWork.TripRepository.GetById(id);
                return Ok(new Response { Status = "Success", MessageEn = "Get Trip Success", MessageAr = "كل الاصناف", Errors = null, Data = Trip });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Getting Trip. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Getting Trip", MessageAr = "حدث خطا اثناء الحصول علي الاصناف", Errors = ex.Message, Data = null });
            }
        }
        /// <summary>
        /// GetBrandByTrip
        ///  api/Trip/GetBrandByTrip
        /// </summary>
        /// <param name="TripId"></param>
        /// <returns>Response</returns>
        [HttpGet()]
        [Route("GetBrandByTripId")]
        public IActionResult GetBrandByTrip(int TripId)
        {
            try
            {
                var Trip = _unitOfWork.TripRepository.Get(n => n.Id == TripId, null, "Brands");
                return Ok(new Response { Status = "Success", MessageEn = "Get Brands By TripId Success", MessageAr = "كل البرندات من خلال الصنف", Errors = null, Data = Trip });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when  Getting Brands By TripId Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Getting Brands By TripId", MessageAr = "حدث خطا اثناء الحصول علي البراندات من خلال الصنف", Errors = ex.Message, Data = null });
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
                return Ok(new Response { Status = "Success", MessageEn = "Insert Trip Success", MessageAr = "تمت عملية اضافة صنف بنجاح", Errors = null, Data = Trip });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Inserting Trip. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Inserting Trip", MessageAr = "حدث خطا اثناء ادخال الصنف", Errors = ex.Message, Data = null });
            }

        }
        /// <summary>
        /// Update
        /// Put:api/Trip/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="TripModel"></param>
        /// <returns>Response</returns>
        [HttpPost]
        [Route("Update")]
        public IActionResult Update(int id, [FromBody] TripModel TripModel)
        {
            try
            {
                    var entityToUpdate = _mapper.Map<Trip>(TripModel);
                entityToUpdate.Id = id;
                _unitOfWork.TripRepository.Update(entityToUpdate);
                _unitOfWork.Commit();
                return Ok(new Response { Status = "Success", MessageEn = "Update Trip Success", MessageAr = "تمت عملية نعديل صنف بنجاح", Errors = null, Data = entityToUpdate });
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Updating Trip. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Updating Trip", MessageAr = "حدث خطا اثناء تعديل الصنف", Errors = ex.Message, Data = null });
            }
        }
        /// <summary>
        /// Delete
        ///   DELETE: api/Trip/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
               
                    _unitOfWork.TripRepository.Delete(id);
                    _unitOfWork.Commit();
                    return Ok(new Response { Status = "Success", MessageEn = "Delete Trip Success", MessageAr = "تمت عملية حذف صنف بنجاح", Errors = null, Data = id });
               
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Trip Status. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Trip Status", MessageAr = "حدث خطا اثناء حذف الصنف", Errors = ex.Message, Data = null });
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
