//HelloWorld.cs

/*
Annotated version of microsoft visual C# hello world example 
*/

using System;	//system namespace, used for console printing

class Hello{	//everything must exist inside a class, like Java
  static void Main(){	/*Static - Main is instance-of-hello independent and can be called without initialising a hello. 
						Only one Main can exist in the program and is used like int main() in c++*/
    Console.WriteLine("Hello, World");	//write to console, similar to C++ iostream's cout
  }
}