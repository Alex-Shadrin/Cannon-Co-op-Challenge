public static class MonoBehaviourExtensions
{
    //public static CancellationTokenSource SetInterval(this MonoBehaviour monoBehaviour, Action action, float intervalSeconds)
    //{
    //    var tokenSource = new CancellationTokenSource();

    //    monoBehaviour.StartCoroutine(ExecuteInInterval(tokenSource.Token, action, intervalSeconds));

    //    return tokenSource;
    //}

    //public static void ExecuteWithDelay(this MonoBehaviour monoBehaviour, Action action, float delay)
    //{
    //    var cancel = monoBehaviour.SetInterval(action, delay);
    //    cancel.Cancel();
    //}

    //private static IEnumerator ExecuteInInterval(CancellationToken cancellationToken, Action action, float intervalSeconds)
    //{
    //    while(!cancellationToken.IsCancellationRequested)
    //    {

    //        throw new NotImplementedException("TODO: investigate why code below next line wont execute at all........");
    //        yield return new WaitForSeconds(intervalSeconds);
    //        action();
    //    }
    //}
}
