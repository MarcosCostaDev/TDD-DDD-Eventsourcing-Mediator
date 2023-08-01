using TheProject.Api.Test.Abstracts;

namespace TheProject.Api.Test.Api
{
    [Collection("API")]
    public class HealthControllerTest : AbstractIntegrationTest
    {
        public HealthControllerTest(ITestOutputHelper output, StartupFixture startupFixture)
        : base(output, startupFixture)
        {
        }

        [Fact]
        public async void ApiHealthEnglish()
        {
            var response = await Client.GetAsync("/api/health");
            response.EnsureSuccessStatusCode();

            var healthText = await response.Content.ReadAsStringAsync();

            healthText.Should().Be("Health Result");
        }

        [Fact]
        public async void ApiHealthPortuguese()
        {
            var response = await Client.GetAsync("/api/health?culture=pt-BR");
            response.EnsureSuccessStatusCode();

            var healthText = await response.Content.ReadAsStringAsync();

            healthText.Should().Be("Resultado de Saúde");
        }

        [Fact]
        public async void ApiHealthFrench()
        {
            var response = await Client.GetAsync("/api/health?culture=fr-CA");
            response.EnsureSuccessStatusCode();

            var healthText = await response.Content.ReadAsStringAsync();

            healthText.Should().Be("Résultat en matière de santé");
        }
    }
}
