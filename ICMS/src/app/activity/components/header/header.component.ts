import { Component, ViewEncapsulation } from '@angular/core';
import { MenuItemModel } from '@syncfusion/ej2-angular-navigations';
import { LocalStorageKeys } from 'src/app/shared/system.constants';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class HeaderComponent {
  public show: boolean = false;

  constructor() {
  }

  toggle() {
    this.show = !this.show;
  }

  // Menu items definition 
  public menuItems: MenuItemModel[] = [
      {
          text: localStorage.getItem(LocalStorageKeys.StudentName),
          iconCss: 'fa fa-user user-nav',
          items: [
            { text: 'Profile', iconCss: 'fa fa-user' },
            { separator: true },
            { text: 'Logout', iconCss: 'fa fa-power-off' }
          ]
      }
  ];
}
