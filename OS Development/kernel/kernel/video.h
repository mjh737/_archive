#ifndef VIDEO_H
#define VIDEO_H

class Video
{
public:
	Video();
	~Video();
	void Clear();
	void Write(char *cp);
	void Put (char c);
private:
	unsigned short *videomem;
	unsigned int off;
	unsigned int pos;
};
#endif 