import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestEditPageComponent } from './quest-edit-page.component';

describe('QuestEditPageComponent', () => {
  let component: QuestEditPageComponent;
  let fixture: ComponentFixture<QuestEditPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuestEditPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuestEditPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
