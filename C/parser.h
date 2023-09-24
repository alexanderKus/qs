#ifndef PARSER_H
#define PARSER_H

typedef struct {
  int lookahead;
} Parser;

void expr(Parser *parser);
void term(Parser *parser);
void match(Parser *parser, int t);

#endif
