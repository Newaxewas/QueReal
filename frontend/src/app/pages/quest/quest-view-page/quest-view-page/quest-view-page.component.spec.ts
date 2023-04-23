import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestViewPageComponent } from './quest-view-page.component';

describe('QuestViewPageComponent', () => {
  let component: QuestViewPageComponent;
  let fixture: ComponentFixture<QuestViewPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ QuestViewPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(QuestViewPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
