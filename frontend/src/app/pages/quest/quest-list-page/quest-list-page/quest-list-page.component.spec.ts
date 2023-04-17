import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestListPageComponent } from './quest-list-page.component';

describe('QuestListPageComponent', () => {
  let component: QuestListPageComponent;
  let fixture: ComponentFixture<QuestListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuestListPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuestListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
