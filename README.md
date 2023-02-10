# FileFormatLibrary

This repository provides a library for working with data records of a specific type. It can add, edit, delete and also convert data to files of different formats and vice versa. The strong side of this library - it is universal tool for working with file conversion. It has only two methods - load records from a file and save records to a file. The user only needs to specify the relative path to the file and the file name with the desired extension. If you try to save data to a file that does not exist, the library will create that file. The weak side are that if you need to start working with a new file, then you need to create a new instance of the library service.

What needs to be fixed/added to the library:
- Add the ability to work with multiple files, including different formats, within a single service instance.
- Improve exception handling.
- Write more tests for different situations.

Possible additional functionality:
- Add more file formats.
- Add conversion between different file formats.
- Try to create universal algorithm of file conversion for any data types.
