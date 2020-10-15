using DiscogsApi.Contrats.Proxys;
using DiscogsApi.Contrats.Services;
using DiscogsApi.Modeles;
using DiscogsApi.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace DiscogsApi.Tests.Services
{
    public class CollectionServiceTest
    {
        private readonly Mock<IDiscogsApiProxy> _mockDiscogsApi = new Mock<IDiscogsApiProxy>();
        private readonly ICollectionService _service;

        public CollectionServiceTest()
        {
            _service = new CollectionService(_mockDiscogsApi.Object);
        }

        [Fact]
        public void ObtenirDisquesAsync_QuantiteNule_RetournUnDisque()
        {
            _mockDiscogsApi.Setup(p => p.ObtenirCollectionDisquesAsync(It.IsAny<int>()))
                .ReturnsAsync(new CollectionDiscogs
                {
                    Pagination = new CollectionDiscogsPagination { Items = 10 },
                    Releases = new List<CollectionDiscogsRelease>()
                });

            var resultat = _service.ObtenirDisquesAsync().Result;

            Assert.Single(resultat);
            _mockDiscogsApi.Verify(p => p.ObtenirCollectionDisquesAsync(It.IsAny<int>()), Times.Exactly(2));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void ObtenirDisquesAsync_AvecQuantite_RetourneQuantiteExacte(int quantite)
        {
            _mockDiscogsApi.Setup(p => p.ObtenirCollectionDisquesAsync(It.IsAny<int>()))
                .ReturnsAsync(new CollectionDiscogs
                {
                    Pagination = new CollectionDiscogsPagination { Items = 10 },
                    Releases = new List<CollectionDiscogsRelease>()
                });

            var resultat = _service.ObtenirDisquesAsync(quantite).Result;

            Assert.Equal(quantite, resultat.Count);
            _mockDiscogsApi.Verify(p => p.ObtenirCollectionDisquesAsync(It.IsAny<int>()), Times.Exactly(quantite + 1));
        }

        [Fact]
        public void ObtenirDisquesAsync_QuantitePlusGrandeLimite_RetourneMaxQuantite()
        {
            int quantite = 10;
            _mockDiscogsApi.Setup(p => p.ObtenirCollectionDisquesAsync(It.IsAny<int>()))
                .ReturnsAsync(new CollectionDiscogs
                {
                    Pagination = new CollectionDiscogsPagination { Items = 10 },
                    Releases = new List<CollectionDiscogsRelease>()
                });

            var resultat = _service.ObtenirDisquesAsync(quantite).Result;

            Assert.Equal(5, resultat.Count);
            _mockDiscogsApi.Verify(p => p.ObtenirCollectionDisquesAsync(It.IsAny<int>()), Times.Exactly(6));
        }

        [Fact]
        public void ObtenirDisquesAsync_QuantitePlusGrandeDisqueDisponible_RetourneNombreDisque()
        {
            int quantite = 5;
            int nbDisqueDispo = 4;
            _mockDiscogsApi.Setup(p => p.ObtenirCollectionDisquesAsync(It.IsAny<int>()))
                .ReturnsAsync(new CollectionDiscogs
                {
                    Pagination = new CollectionDiscogsPagination { Items = nbDisqueDispo },
                    Releases = new List<CollectionDiscogsRelease>()
                });

            var resultat = _service.ObtenirDisquesAsync(quantite).Result;

            Assert.Equal(nbDisqueDispo, resultat.Count);
            _mockDiscogsApi.Verify(p => p.ObtenirCollectionDisquesAsync(It.IsNotNull<int>()), Times.Exactly(nbDisqueDispo + 1));
        }
    }
}
