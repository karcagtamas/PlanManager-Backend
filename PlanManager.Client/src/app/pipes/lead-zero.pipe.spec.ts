import { LeadZeroPipe } from './lead-zero.pipe';

describe('LeadZeroPipe', () => {
  it('create an instance', () => {
    const pipe = new LeadZeroPipe();
    expect(pipe).toBeTruthy();
  });
});
