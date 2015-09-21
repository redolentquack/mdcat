# mdcat

> Super simple console highlighting for Markdown (style) files.

That is of course if the console you are using will support the colours!

## Usage

"cat" the markdown file:

    mdcat filename

e.g.

    mdcat ReadMe.md

You get some extra colour for headers, quotes, code etc.

That's it, super simple `;-)`


Basic Support
-------------

Like I said it is simple support for Markdown style syntax, not full. 
Two types of heading styles are covered, the `===` and `---` style as 
above and the `#` prefix style.

> Quoted text is covered.

As usual the new lines between the text "reset" the style.

    /* Code style with the 4 spaces or a tab */
    someCode.do();

In addition:

```
// The tripple back-tick is covered...
```

I left *bold* etc alone as they get messy quick, you have lists and then
**emphasis** and then there is ***bold emphasis*** and you can also *go
across lines* and my goal was not a token parser but a quick bit of
colour.