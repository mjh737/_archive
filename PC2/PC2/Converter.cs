namespace PC2
{
    public class Converter
    {
        public static byte ConvertBitsToByte(string bits)
        {
            byte b = 0;

            if (bits.Substring(0, 1) == "1") b += 128;
            if (bits.Substring(1, 1) == "1") b += 64;
            if (bits.Substring(2, 1) == "1") b += 32;
            if (bits.Substring(3, 1) == "1") b += 16;
            if (bits.Substring(4, 1) == "1") b += 8;
            if (bits.Substring(5, 1) == "1") b += 4;
            if (bits.Substring(6, 1) == "1") b += 2;
            if (bits.Substring(7, 1) == "1") b += 1;

            return b;
        }

        public static char ConvertByteToChar(byte b)
        {
            if (b == 0) return '\0';
            if (b == 10) return '\n';
            if (b == 13) return '\r';
            if (b == 32) return ' ';
            if (b == 65) return 'A';
            if (b == 66) return 'B';
            if (b == 67) return 'C';
            if (b == 68) return 'D';
            if (b == 69) return 'E';
            if (b == 70) return 'F';
            if (b == 71) return 'G';
            if (b == 72) return 'H';
            if (b == 73) return 'I';
            if (b == 74) return 'J';
            if (b == 75) return 'K';
            if (b == 76) return 'L';
            if (b == 77) return 'M';
            if (b == 78) return 'N';
            if (b == 79) return 'O';
            if (b == 80) return 'P';
            if (b == 81) return 'Q';
            if (b == 82) return 'R';
            if (b == 83) return 'S';
            if (b == 84) return 'T';
            if (b == 85) return 'U';
            if (b == 86) return 'V';
            if (b == 87) return 'W';
            if (b == 88) return 'X';
            if (b == 89) return 'Y';
            if (b == 90) return 'Z';
            if (b == 97) return 'a';
            if (b == 98) return 'b';
            if (b == 99) return 'c';
            if (b == 100) return 'd';
            if (b == 101) return 'e';
            if (b == 102) return 'f';
            if (b == 103) return 'g';
            if (b == 104) return 'h';
            if (b == 105) return 'i';
            if (b == 106) return 'j';
            if (b == 107) return 'k';
            if (b == 108) return 'l';
            if (b == 109) return 'm';
            if (b == 110) return 'n';
            if (b == 111) return 'o';
            if (b == 112) return 'p';
            if (b == 113) return 'q';
            if (b == 114) return 'r';
            if (b == 115) return 's';
            if (b == 116) return 't';
            if (b == 117) return 'u';
            if (b == 118) return 'v';
            if (b == 119) return 'w';
            if (b == 120) return 'x';
            if (b == 121) return 'y';
            if (b == 122) return 'z';

            return '?';
        }

        public static byte ConvertCharToByte(char c)
        {
            if (c == '\n') return 10;
            if (c == '\r') return 13;
            if (c == '_') return 32;
            if (c == ' ') return 32;
            if (c == 'M') return 77;
            if (c == 'O') return 79;
            if (c == 'S') return 83;
            if (c == 'a') return 97;
            if (c == 'b') return 98;
            if (c == 't') return 116;
            
            

            return 0;
        }
    }
}
