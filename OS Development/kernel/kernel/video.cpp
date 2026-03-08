#include "video.h"

Video::Video()
{
	pos = 0;
	off = 0;
	videomem = (unsigned short*) 0xb8000;
}

Video::~Video()
{
}

void Video::Clear()
{
	unsigned int i;

	for (i = 0; i < (80 * 25); i++)
	{
		videomem[i] = (unsigned char)' ' | 0x0700;
	}

	pos = 0;
	off = 0;
}

void Video::Write(char *cp)
{
	char *str = cp, *ch;

	for (ch = str; *ch;ch++)
	{
		Put (*ch);
	}
}

void Video::Put(char c)
{
	if (pos >= 80) 
	{
		pos = 0;
		off += 80;
	}

	if (off >= 80 * 25)
	{
		Clear();
	}

	videomem[off + pos] = (unsigned char) c | 0x0700;
	pos++;
}



