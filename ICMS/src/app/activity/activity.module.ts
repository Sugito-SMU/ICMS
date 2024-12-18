import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivityRoutingModule } from './/activity-routing.module';

import { ActivityComponent } from './activity.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { MainmenuComponent } from './components/mainmenu/mainmenu.component';
import { SelectActivityComponent } from './select-activity/select-activity.component';
import { ActivityDetailComponent } from './activity-detail/activity-detail.component';
import { ActivitySummaryComponent } from './activity-detail/activity-summary/activity-summary.component';
import { PreReflectionComponent } from './activity-detail/pre-reflection/pre-reflection.component';
import { LeaningObjectiveQnaComponent } from './activity-detail/leaning-objective-qna/leaning-objective-qna.component';

import { GridModule } from '@syncfusion/ej2-angular-grids';
import { ButtonModule, RadioButtonModule } from '@syncfusion/ej2-angular-buttons';
import { TabModule } from '@syncfusion/ej2-angular-navigations';
import { AccordionModule } from '@syncfusion/ej2-angular-navigations';
import { MenuModule } from '@syncfusion/ej2-angular-navigations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DialogModule } from '@syncfusion/ej2-angular-popups';
import { TextBoxModule } from '@syncfusion/ej2-angular-inputs';
import { DropDownListModule } from '@syncfusion/ej2-angular-dropdowns';
import { CheckBoxModule } from '@syncfusion/ej2-angular-buttons';
import { TooltipModule } from '@syncfusion/ej2-angular-popups';

import { PageService, SortService, FilterService, GroupService } from '@syncfusion/ej2-angular-grids';
import { AggregateService } from '@syncfusion/ej2-angular-grids';
import { PostReflectionQuestionsComponent } from './activity-detail/post-reflection-questions/post-reflection-questions.component';
import { SafeHTMLPipe } from '../shared/pipes/safe-html.pipe';
import { ReplaceLineBreaks } from '../shared/pipes/replaceLineBreaks';
import { ToastrModule } from 'ngx-toastr';
import { SmuLoaderComponent } from './components/smu-loader/smu-loader.component';
import { AccessDeniedComponent } from './components/access-denied/access-denied.component';

@NgModule({
  imports: [
    CommonModule,
    ActivityRoutingModule,
    GridModule,
    TabModule,
    ButtonModule,
    AccordionModule,
    MenuModule,
    FormsModule,
    ReactiveFormsModule,
    DialogModule,
      TextBoxModule,
      DropDownListModule,
      RadioButtonModule,
      CheckBoxModule,
      TooltipModule
  ],
    declarations: [ActivityComponent, HeaderComponent, FooterComponent, MainmenuComponent, SelectActivityComponent, ActivityDetailComponent, ActivitySummaryComponent, PreReflectionComponent, LeaningObjectiveQnaComponent, PostReflectionQuestionsComponent, SafeHTMLPipe, ReplaceLineBreaks, SmuLoaderComponent, AccessDeniedComponent],
    providers: [PageService, SortService, FilterService, GroupService, AggregateService]
})
export class ActivityModule { }
