using Nett.Tests.Util;

namespace Nett.Tests.Functional
{
    public class PreserveSourceToml
    {
        private const string FuncWriteMerged = "Write Merged";

        public class Config
        {
            [TomlComment("Originally defined comment.")]
            public int X { get; set; } = 2;
        }

        [FFact(FuncWriteMerged, "When merge target comment was changed, leaves comment intact but updates value")]
        public void WriteMerged_WhenCommentInMergeTargetWasChanged_UpdatesValueButLeavesCommentIntact()
        {
            // Arrange
            const string src = @"
# This comment was changed by a user in the file
X = 1
";
            var c = new Config();
            string expected = src.Replace("= 1", $"= {c.X}");

            // Act
            var newToml = Toml.WriteStringMerged(new Config(), src);

            // Assert
            newToml.ShouldBeSemanticallyEquivalentTo(expected);

        }
    }
}
