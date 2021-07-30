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
    public class ReservationController : ControllerBase
    {
        #region Property

        private readonly ILogger<ReservationController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor

        public ReservationController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<ReservationController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #endregion

        #region Reservations

        /// <summary>
        /// GetReservations
        ///  GET: api/Reservation
        /// </summary>
        /// <returns>Response</returns>
        [HttpGet]
        public IActionResult GetReservations(int Index, int count)
        {
            try
            {
                var Reservations = _unitOfWork.ReservationRepository.GetPerPage((Index-1)* count, count,null,null, "Trip");
                return Ok(new Response { Status = "Success", MessageEn = "Get Reservations Success", MessageAr = "كل الحجوزات", Errors = null, Data = Reservations });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Getting Reservations. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Getting Reservations", MessageAr = "حدث خطا اثناء الحصول علي الحجوزات", Errors = ex.Message, Data = null });
            }

        }
        /// <summary>
        /// GetReservation
        ///  GET: api/Reservation/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Response</returns>
        [HttpGet("{id}")]
        public IActionResult GetReservation(int id)
        {
            try
            {
                var Reservation = _unitOfWork.ReservationRepository.GetById(id);
                return Ok(new Response { Status = "Success", MessageEn = "Get Reservation Success", MessageAr = "كل الحجوزات", Errors = null, Data = Reservation });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Getting Reservation. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Getting Reservation", MessageAr = "حدث خطا اثناء الحصول علي الحجوزات", Errors = ex.Message, Data = null });
            }
        }
        /// <summary>
        /// Add
        ///  POST: api/Reservation
        /// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// </summary>
        /// <param name="ReservationModel"></param>
        /// <returns>Response</returns>
        [HttpPost]
        public IActionResult Add([FromBody] ReservationModel ReservationModel)
        {
            try
            {
                var Reservation = _mapper.Map<Reservation>(ReservationModel);
               _unitOfWork.ReservationRepository.Insert(Reservation);
                _unitOfWork.Commit();
                return Ok(new Response { Status = "Success", MessageEn = "Insert Reservation Success", MessageAr = "تمت عملية اضافة حجز بنجاح", Errors = null, Data = Reservation });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Inserting Reservation. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Inserting Reservation", MessageAr = "حدث خطا اثناء ادخال الحجز", Errors = ex.Message, Data = null });
            }

        }
        /// <summary>
        /// Update
        /// Put:api/Reservation/{id}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ReservationModel"></param>
        /// <returns>Response</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ReservationModel ReservationModel)
        {
            try
            {
                    var entityToUpdate = _mapper.Map<Reservation>(ReservationModel);
                entityToUpdate.Id = id;
                _unitOfWork.ReservationRepository.Update(entityToUpdate);
                _unitOfWork.Commit();
                return Ok(new Response { Status = "Success", MessageEn = "Update Reservation Success", MessageAr = "تمت عملية نعديل حجز بنجاح", Errors = null, Data = entityToUpdate });
                
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Updating Reservation. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Updating Reservation", MessageAr = "حدث خطا اثناء تعديل الحجز", Errors = ex.Message, Data = null });
            }
        }
        /// <summary>
        /// Delete
        ///   DELETE: api/Reservation/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
               
                    _unitOfWork.ReservationRepository.Delete(id);
                    _unitOfWork.Commit();
                    return Ok(new Response { Status = "Success", MessageEn = "Delete Reservation Success", MessageAr = "تمت عملية حذف حجز بنجاح", Errors = null, Data = id });
               
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when Reservation Status. Error: {}", ex.Message);
                _unitOfWork.Rollback();
                return Ok(new Response { Status = "Error", MessageEn = "Error when Reservation Status", MessageAr = "حدث خطا اثناء حذف الحجز", Errors = ex.Message, Data = null });
            }
        }
        /// <summary>
        /// ReservationExists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ReservationExists(int id)
        {
            if( _unitOfWork.ReservationRepository.GetById(id)!=null)
            {
                return true;
            }
            else { return true; }
        }

        #endregion

    }
}
