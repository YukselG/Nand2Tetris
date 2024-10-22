// This file is part of www.nand2tetris.org
// and the book "The Elements of Computing Systems"
// by Nisan and Schocken, MIT Press.
// File name: projects/4/Fill.asm

// Runs an infinite loop that listens to the keyboard input. 
// When a key is pressed (any key), the program blackens the screen,
// i.e. writes "black" in every pixel. When no key is pressed, 
// the screen should be cleared.

(INFINITE)
@endScreenBlack
M=0
@endScreenWhite
M=0
// Screen address
@SCREEN
D=A
// Assign BaseScreen = 16384
@baseScreen
M=D

@indexblack
M=0

@indexwhite
M=0

// Check if any key is pressed, so checking KBD value (value is 0 when no key is pressed)
@KBD
D=M
@BLACKSCREEN
D;JNE
@WHITESCREEN
D;JEQ

(BLACKSCREEN)
@KBD
D=M
@WHITESCREEN
D;JEQ
@endScreenBlack
D=M
@KBD
D=A-D
@INFINITE
D;JEQ
@baseScreen
D=M
@indexblack
A=D+M
M=-1
D=A
@endScreenBlack
M=D
@indexblack
M=M+1
@endScreenBlack
D=M
@KBD
D=A-D
@BLACKSCREEN
D;JNE

(WHITESCREEN)
@KBD
D=M
@BLACKSCREEN
D;JNE
@endScreenWhite
D=M
@KBD
D=D-A
@INFINITE
D;JEQ
@baseScreen
D=M
@indexwhite
A=D+M
M=0
D=A
@endScreenWhite
M=D
@indexwhite
M=M+1
@endScreenWhite
D=M
@KBD
D=A-D
@WHITESCREEN
D;JNE