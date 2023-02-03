using System.Collections;

namespace Ui
{
    public class UiScaleShakerConstant : UiScaleShaker
    {
        protected override IEnumerator AnimateSize()
        {
            while (true)
            {
                yield return base.AnimateSize();
                yield return null;
            }
        }

        private void Start()
        {
            ShakeScale();
        }
    }
}