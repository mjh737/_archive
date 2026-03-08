namespace ZX81
{
    using static ZX81.Enums;

    public record Token
    {
        public TokenType TokenType { get; set; }
        public string Value { get; set; }
        public string Identifier { get; set; }

        public Token(TokenType tt, string identifier)
        {
            TokenType = tt;
            Identifier = identifier;
        }
    }
}