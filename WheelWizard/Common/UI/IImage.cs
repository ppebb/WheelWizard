using System.IO;

namespace CT_MKWII.Common.UI;

public interface ICreateImage
{
    IImage NewImage(string url);
}

public interface IImage { }
