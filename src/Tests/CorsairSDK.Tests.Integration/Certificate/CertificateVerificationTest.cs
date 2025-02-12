namespace CorsairSDK.Tests.Integration.Certificate;

using Corsair.Device.Internal;
using FluentAssertions;

[TestFixture(Author = "JustArion", Category = "Certificate", Description = "Verifies if the underlying native SDK is signed and verified.")]
public class CertificateVerificationTest
{
    private DirectoryInfo  _testDirectory;
    [SetUp]
    public void Setup()
    {
        _testDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()));
    }
    
    [TearDown]
    public void Cleanup()
    {
        _testDirectory.Delete(true);
    }
    
    [Test]
    public void X64Binary_ShouldBe_Valid()
    {
        var binaryPath = Path.Combine(_testDirectory.FullName, SDKResolver.X64_FILENAME);
        SDKResolver.ExtractBinary(SDKResolver.X64_FILENAME, binaryPath)
            .Should().BeTrue();

        SDKResolver.IsSignedAndVerified(binaryPath)
            .Should().BeTrue();
    }
    
    [Test]
    public void X86Binary_ShouldBe_Valid()
    {
        var binaryPath = Path.Combine(_testDirectory.FullName, SDKResolver.X86_FILENAME);
        SDKResolver.ExtractBinary(SDKResolver.X86_FILENAME, binaryPath)
            .Should().BeTrue();

        SDKResolver.IsSignedAndVerified(binaryPath)
            .Should().BeTrue();
    }
}