using DiscogsApi.Contrats.Services;
using DiscogsApi.Modeles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscogsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Disque>>> ObtenirDisques([FromQuery] int? quantite)
        {
            try
            {
                var disques = await _collectionService.ObtenirDisquesAsync(quantite);
                return Ok(disques);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Une erreur est survenue lors de la récuperation de la liste de disques");
            }
        }
    }
}
