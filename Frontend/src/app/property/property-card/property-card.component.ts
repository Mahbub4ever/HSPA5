import { Component } from '@angular/core';

@Component({
  selector: 'aap-property-card',
  // template: `<h1>I am a card</h1>`,
  templateUrl: 'property-card.component.html',
  // styles: ['h1{font-weight:normal;}']
  styleUrls: ['property-card.component.css']
})
export class PropertyCardComponent {
  Property: any = {
    "Id":13,
    "Name":"Ayan House",
    "Type":"Villa",
    "Price":130000 //test
  }

}
