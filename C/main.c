#include <stdio.h>
#include <stdlib.h>

#include "parser.h"

int main(void) {
  Parser *parser = (Parser *)malloc(sizeof(Parser));

  scanf("%d" &parser->lookahead);
  expr(parser);

  free(parser);
  return 0;
}
