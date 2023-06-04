import {Component, OnInit} from '@angular/core';
import {BaseComponent} from 'src/app/common/base.component';
import {Permission} from "../../models/enums/permission.enum";
import {ContentService} from "../../services/content.service";
import {Content} from "../../models/content.model";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent extends BaseComponent implements OnInit {
  contentValue: string;
  contentType: string;
  permissions = Permission;
  htmlEditor?: any;

  constructor(private contentService: ContentService) {
    super();
    this.contentValue = "";
    this.contentType = "html";
  }

  ngOnInit(): void {
    this.loadContent();
  }

  loadContent() {
    this.subscribe(this.contentService.getHomePage(), {
      next: (result: Content) => this.contentValue = result.value
    });
  }

  saveContent(){
    this.subscribe(this.contentService.createHomePage(this.contentValue), {
      next: () => {
        this.notificationService.show('The content of the home page has been successfully saved', 'success');
        this.loadContent();
      },
      error: () => this.notificationService.show('Failed to save home page content', 'error')
    });
  }
}
