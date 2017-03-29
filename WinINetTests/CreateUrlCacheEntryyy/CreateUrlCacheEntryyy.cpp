// CreateUrlCacheEntryyy.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <ios>
#include <stdio.h>
#include <WinInet.h>


int main()
{
	CString strURL;
	m_url.GetWindowText(strURL);
	char lpszFileName[MAX_PATH + 1];
	//create a cache entry for the url
	if (CreateUrlCacheEntry(strURL, 0, "htm",
		lpszFileName, 0) == TRUE)
	{
		/* //This would save to a local file
		//I did not use this function because I am behind a proxy
		//and the webcontrol does not work from behind a proxy
		//or if it does I could not find out how to make it work
		URLDownloadToFile(0,strURL,lpszFileName,0,0);
		//Alternatively you could skip everything after this
		//and just use URLDownloadToCacheFile() */


		//But we copy the local file to the cache file
		char ch;

		//Open the file for reading.
		std::ifstream fp_read("testme.html", std::ios_base::in);
		//open the cache file for writing
		std::ofstream fp_write(lpszFileName, std::ios_base::out);

		while (fp_read.eof() != true)
		{
			fp_read.get(ch);
			//Check for CR (carriage return)
			if ((int)ch == 0x0D)
				continue;
			if (!fp_read.eof()) fp_write.put(ch);
		}
		fp_read.close();
		fp_write.close();
		//create a null expire and modified time
		FILETIME ftExpireTime;
		ftExpireTime.dwHighDateTime = 0;
		ftExpireTime.dwLowDateTime = 0;
		FILETIME ftModifiedTime;
		ftModifiedTime.dwHighDateTime = 0;
		ftModifiedTime.dwLowDateTime = 0;
		//commit the cache entry
		//the HTTP/1.0 200 OK\r\n\r\n should be 
		//used for the header or else IE will not read it
		if (CommitUrlCacheEntry(strURL, lpszFileName,
			ftExpireTime, ftModifiedTime,
			NORMAL_CACHE_ENTRY,
			(LPBYTE)"HTTP/1.0 200 OK\r\n\r\n",
			20, NULL, NULL) == TRUE)
		{
			//Navigate to the local file
			m_browser.Navigate(lpszFileName, NULL,
				NULL, NULL, NULL);
		}
	}
}

