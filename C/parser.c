#include <stdio.h>
#include <ctype.h>

#include "parser.h"

void expr(Parser *parser) {
  term(parser);
  while(1) {
    if (parser->lookahead == '+') {
      match(parser, '+');
      term(parser);
      fprintf(stdout, "+");
    } else if (parser->lookahead == '-') {
      match(parser, '-');
      term(parser);
      fprintf(stdout, "-");
    } else {
      return;
    }
  }
}

void term(Parser *parser) {
  if (isdigit(parser->lookahead)) {
    fprintf(stdout, "%d", parser->lookahead);
    match(parser, parser->lookahead);
  } else {
    fprintf(stderr, "Syntax error\n");
    exit(1);
  }
}

void match(Parser *parser, int t) {
  if (parser->lookahead == t) {
    scanf("%d", &parser->lookahead);
  } else {
    fprintf(stderr, "Syntax error\n");
    exit(1);
  }
}
