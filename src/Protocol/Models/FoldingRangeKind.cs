using System.Runtime.Serialization;

namespace OmniSharp.Extensions.LanguageServer.Protocol.Models
{
    /// <summary>
    /// Enum of known range kinds
    /// </summary>
    public enum FoldingRangeKind
    {
        /// <summary>
        /// Folding range for a comment
        /// </summary>
	    [EnumMember(Value="comment")]
        Comment,
        /// <summary>
        /// Folding range for a imports or includes
        /// </summary>
	    [EnumMember(Value="imports")]
        Imports,
        /// <summary>
        /// Folding range for a region (e.g. `#region`)
        /// </summary>
	    [EnumMember(Value="region")]
        Region
    }
}
