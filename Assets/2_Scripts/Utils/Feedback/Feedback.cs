public static class Feedback
{
    public static void Do(eFeedbackType type)
    {
        switch (type)
        {
			case eFeedbackType.RollDice:
			case eFeedbackType.Punch:
			case eFeedbackType.Punch2:
			case eFeedbackType.Piano:
				SFXPlayer.PlaySFX(type);
				break;
			
			case eFeedbackType.Menu:
			case eFeedbackType.Juego:
				SFXPlayer.PlayBGM(type);
				break;
        }
    }
}