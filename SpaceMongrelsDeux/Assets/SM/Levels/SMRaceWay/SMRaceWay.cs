namespace SM
{
  public class SMRaceWay : SMLevelExterior
  {
    public string introAnimation;

    protected override void onLevelBegin()
    {
      base.onLevelBegin();

      // game.view.setTarget( levelPawns[currentLevelPawnIndex].transform, true );
      game.view.playAnimation(introAnimation);
    }
  }
}
