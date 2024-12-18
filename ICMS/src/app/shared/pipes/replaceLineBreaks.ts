import { Pipe, PipeTransform } from '@angular/core';
@Pipe({name: 'replaceLineBreaks'})
export class ReplaceLineBreaks implements PipeTransform {
    transform(value: string): string {
        return value.replace(/\r\n/g, "<br/>").replace(/\n\r/g, "<br/>").replace(/\n/g, "<br/>").replace(/\r/g, "<br/>");
   }
}