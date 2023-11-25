using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiBookingAPI.Data;
using SkiBookingAPI.Data.Models;

namespace SkiBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public BasketItemsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/BasketItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketItem>>> GetBasketItems()
        {
            if (_context.BasketItem == null)
            {
                return NotFound();
            }
            return await _context.BasketItem.ToListAsync();
        }

        // GET: api/BasketItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketItem>> GetBasketItem(long id)
        {
            if (_context.BasketItem == null)
            {
                return NotFound();
            }
            var basketItem = await _context.BasketItem.FindAsync(id);

            if (basketItem == null)
            {
                return NotFound();
            }

            return basketItem;
        }

        // PUT: api/BasketItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBasketItem(long id, BasketItem basketItem)
        {
            if (id != basketItem.BasketItemID)
            {
                return BadRequest();
            }

            _context.Entry(basketItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BasketItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BasketItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BasketItem>> PostBasketItem(BasketItem basketItem)
        {
            if (_context.BasketItem == null)
            {
                return Problem("Entity set 'ApplicationDBContext.BasketItems'  is null.");
            }
            _context.BasketItem.Add(basketItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBasketItem", new { id = basketItem.BasketItemID }, basketItem);
        }

        // DELETE: api/BasketItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasketItem(long id)
        {
            if (_context.BasketItem == null)
            {
                return NotFound();
            }
            var basketItem = await _context.BasketItem.FindAsync(id);
            if (basketItem == null)
            {
                return NotFound();
            }

            _context.BasketItem.Remove(basketItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BasketItemExists(long id)
        {
            return (_context.BasketItem?.Any(e => e.BasketItemID == id)).GetValueOrDefault();
        }

        // GET: api/BasketItems/GetBasket
        [HttpGet]
        [Route("GetBasket")]
        public async Task<ActionResult<Basket>> GetBasket()
        {
            if (_context.BasketItem == null)
            {
                return NotFound();
            }

            var packageList =
                from basketItem in _context.BasketItem
                join package in _context.Package on basketItem.PackageID equals package.PackageID
                select new Basket.Item() {
                    BasketItemID = basketItem.BasketItemID,
                    Description = package.Description,
                    Name = package.Name,
                    NumDays = null, //packages have no days
                    ObjectType = "Package",
                    Quantity = basketItem.Quantity,
                    TotalCost = package.TotalCost * basketItem.Quantity,
                    TotalGST = package.TotalGST * basketItem.Quantity
                };

            var equipmentList =
                from basketItem in _context.BasketItem
                join equipment in _context.Equipment on basketItem.EquipmentID equals equipment.EquipmentID
                select new Basket.Item()
                {
                    BasketItemID = basketItem.BasketItemID,
                    Description = equipment.Description,
                    Name = equipment.Name,
                    NumDays = basketItem.NumDays,
                    ObjectType = "Equipment Rental",
                    Quantity = basketItem.Quantity,
                    TotalCost = equipment.TotalCostPerDay * basketItem.Quantity * (basketItem.NumDays.HasValue? basketItem.NumDays.Value : 1),
                    TotalGST = equipment.TotalGSTPerDay * basketItem.Quantity * (basketItem.NumDays.HasValue ? basketItem.NumDays.Value : 1)
                };


            var liftTicketList =
                from basketItem in _context.BasketItem
                join liftTicket in _context.LiftTicket on basketItem.LiftTicketID equals liftTicket.LiftTicketID
                select new Basket.Item()
                {
                    BasketItemID = basketItem.BasketItemID,
                    Description = liftTicket.Description,
                    Name = liftTicket.Name,
                    NumDays = basketItem.NumDays,
                    ObjectType = "Lift Ticket",
                    Quantity = basketItem.Quantity,
                    TotalCost = liftTicket.TotalCostPerDay * basketItem.Quantity * (basketItem.NumDays.HasValue ? basketItem.NumDays.Value : 1),
                    TotalGST = liftTicket.TotalGSTPerDay * basketItem.Quantity * (basketItem.NumDays.HasValue ? basketItem.NumDays.Value : 1)
                };


            var basketItemList = new List<Basket.Item>();
            basketItemList.AddRange(await packageList.ToListAsync());
            basketItemList.AddRange(await equipmentList.ToListAsync());
            basketItemList.AddRange(await liftTicketList.ToListAsync());

            var totalCost = basketItemList.Sum(item => item.TotalCost);
            var totalGST = basketItemList.Sum(item => item.TotalGST);

            var basket = new Basket()
            {
                BasketItems = basketItemList.OrderBy(item => item.BasketItemID).ToArray(),
                TotalCost = totalCost,
                TotalGST = totalGST
            };
            return basket;
        }

        // GET: api/BasketItems/GetBasketItemCount
        [HttpGet]
        [Route("GetBasketItemCount")]
        public async Task<ActionResult<int>> GetBasketItemCount() => await _context.BasketItem.CountAsync();
    }
}
