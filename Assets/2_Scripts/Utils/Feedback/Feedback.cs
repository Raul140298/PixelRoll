public static class Feedback
{
    public static void Do(eFeedbackType type)
    {
        switch (type)
        {
			case eFeedbackType.ButtonClick:
			case eFeedbackType.ButtonHover:	
				SFXPlayer.PlaySFX(type);
				break;
        }
    }
}