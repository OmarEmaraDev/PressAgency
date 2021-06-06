using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace PressAgency.Utils {
  public static class IFormFileExtension {
    public static bool IsPNGImage(this IFormFile file) {
      Stream stream = file.OpenReadStream();
      using (BinaryReader reader = new BinaryReader(stream)) {
        byte[] signature =
            new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
        byte[] headerBytes = reader.ReadBytes(signature.Length);
        return headerBytes.SequenceEqual(signature);
      }
    }
  }
}
