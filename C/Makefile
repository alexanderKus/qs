CC = gcc
CFLAGS = -Wall
SRC = main.c parser.c
OBJ = $(SRC:.c=.o)
EXE = qsc

all: $(EXE)

$(EXE): $(OBJ)
	$(CC) $(CFLAGS) -o $(EXE) $(OBJ)

%.o: %.c
	$(CC) $(CFLAGS) -c $< -o $@

clean:
	rm -f $(EXE) $(OBJ)

