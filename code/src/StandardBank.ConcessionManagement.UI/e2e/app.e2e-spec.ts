import { StandardBank.ConcessionManagement.UIPage } from './app.po';

describe('standard-bank.concession-management.ui App', () => {
  let page: StandardBank.ConcessionManagement.UIPage;

  beforeEach(() => {
    page = new StandardBank.ConcessionManagement.UIPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
