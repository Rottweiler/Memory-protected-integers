# Memory-protected-integers
Memory protected integers (C#)

Why memory protected integers? Because they are harder to hack with software like (for example) Cheat Engine to change the value.

# Usage
It's extremely simple to use
```c#
MemoryProtectedInt<byte> protected_byte = new MemoryProtectedInt<byte>((byte)255);

Console.WriteLine(protected_byte.GetValue());