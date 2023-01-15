using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Planday.Schedule.Api.Models;
using Planday.Schedule.Services;

namespace Planday.Schedule.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftsService _shiftsService;
        private readonly IValidator<ShiftModel> _validator;
        private readonly IMapper _mapper;
        
        public ShiftController(IShiftsService shiftsService,
            IValidator<ShiftModel> validator,
            IMapper mapper)
        {
            _shiftsService = shiftsService;
            _validator = validator;
            _mapper = mapper;
        }
        
        [HttpGet("{shiftId}")]
        public async Task<IActionResult> GetShiftAsync(long shiftId, CancellationToken cancellationToken)
        {
            var shift = await _shiftsService.GetShiftAsync(shiftId, cancellationToken);
            return Ok(shift);
        }
        
        [HttpPost("")]
        public async Task<IActionResult> AddShiftAsync(ShiftModel shiftModel, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(shiftModel, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.FirstOrDefault()?.ErrorMessage);
            }

            var shift = _mapper.Map<Shift>(shiftModel);
            var result = await _shiftsService.AddShiftAsync(shift);
            
            return result ? Ok() : BadRequest();
        }
        
        [HttpPost("Assign")]
        public async Task<IActionResult> AssignShiftAsync(long shiftId, long employeeId, CancellationToken cancellationToken)
        {
            var result = await _shiftsService.AssignShiftAsync(shiftId, employeeId, cancellationToken);
            
            return result ? Ok() : BadRequest();
        }
    }    
}

