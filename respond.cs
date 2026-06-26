using System.Collections;

namespace POE_Check
{
    public class respond
    {

        public respond(ArrayList reply, ArrayList ignore)
        {
            answers(reply);
            words(ignore);
        }

        private void words(ArrayList ignoring)
        {
            ignoring.Add("a");
            ignoring.Add("about");
            ignoring.Add("above");
            ignoring.Add("across");
            ignoring.Add("after");
            ignoring.Add("afterwards");
            ignoring.Add("again");
            ignoring.Add("against");
            ignoring.Add("all");
            ignoring.Add("almost");
            ignoring.Add("alone");
            ignoring.Add("along");
            ignoring.Add("already");
            ignoring.Add("also");
            ignoring.Add("although");
            ignoring.Add("always");
            ignoring.Add("am");
            ignoring.Add("among");
            ignoring.Add("amongst");
            ignoring.Add("amount");
            ignoring.Add("an");
            ignoring.Add("and");
            ignoring.Add("another");
            ignoring.Add("any");
            ignoring.Add("anyhow");
            ignoring.Add("anyone");
            ignoring.Add("anything");
            ignoring.Add("anyway");
            ignoring.Add("anywhere");
            ignoring.Add("are");
            ignoring.Add("around");
            ignoring.Add("as");
            ignoring.Add("at");
            ignoring.Add("back");
            ignoring.Add("be");
            ignoring.Add("became");
            ignoring.Add("because");
            ignoring.Add("become");
            ignoring.Add("becomes");
            ignoring.Add("becoming");
            ignoring.Add("been");
            ignoring.Add("before");
            ignoring.Add("beforehand");
            ignoring.Add("behind");
            ignoring.Add("being");
            ignoring.Add("below");
            ignoring.Add("beside");
            ignoring.Add("besides");
            ignoring.Add("between");
            ignoring.Add("beyond");
            ignoring.Add("both");
            ignoring.Add("but");
            ignoring.Add("by");
            ignoring.Add("can");
            ignoring.Add("cannot");
            ignoring.Add("could");
            ignoring.Add("did");
            ignoring.Add("do");
            ignoring.Add("does");
            ignoring.Add("doing");
            ignoring.Add("done");
            ignoring.Add("down");
            ignoring.Add("during");
            ignoring.Add("each");
            ignoring.Add("either");
            ignoring.Add("else");
            ignoring.Add("elsewhere");
            ignoring.Add("enough");
            ignoring.Add("etc");
            ignoring.Add("even");
            ignoring.Add("ever");
            ignoring.Add("every");
            ignoring.Add("everyone");
            ignoring.Add("everything");
            ignoring.Add("everywhere");
            ignoring.Add("except");
            ignoring.Add("few");
            ignoring.Add("first");
            ignoring.Add("for");
            ignoring.Add("former");
            ignoring.Add("formerly");
            ignoring.Add("from");
            ignoring.Add("further");
            ignoring.Add("had");
            ignoring.Add("has");
            ignoring.Add("have");
            ignoring.Add("having");
            ignoring.Add("he");
            ignoring.Add("hence");
            ignoring.Add("her");
            ignoring.Add("here");
            ignoring.Add("hereafter");
            ignoring.Add("hereby");
            ignoring.Add("herein");
            ignoring.Add("hereupon");
            ignoring.Add("hers");
            ignoring.Add("herself");
            ignoring.Add("him");
            ignoring.Add("himself");
            ignoring.Add("his");
            ignoring.Add("how");
            ignoring.Add("however");
            ignoring.Add("i");
            ignoring.Add("if");
            ignoring.Add("in");
            ignoring.Add("indeed");
            ignoring.Add("inside");
            ignoring.Add("instead");
            ignoring.Add("into");
            ignoring.Add("is");
            ignoring.Add("it");
            ignoring.Add("its");
            ignoring.Add("itself");
            ignoring.Add("last");
            ignoring.Add("later");
            ignoring.Add("latter");
            ignoring.Add("latterly");
            ignoring.Add("least");
            ignoring.Add("less");
            ignoring.Add("lot");
            ignoring.Add("many");
            ignoring.Add("may");
            ignoring.Add("me");
            ignoring.Add("meanwhile");
            ignoring.Add("might");
            ignoring.Add("more");
            ignoring.Add("moreover");
            ignoring.Add("most");
            ignoring.Add("mostly");
            ignoring.Add("much");
            ignoring.Add("must");
            ignoring.Add("my");
            ignoring.Add("myself");
            ignoring.Add("name");
            ignoring.Add("namely");
            ignoring.Add("neither");
            ignoring.Add("never");
            ignoring.Add("nevertheless");
            ignoring.Add("next");
            ignoring.Add("no");
            ignoring.Add("nobody");
            ignoring.Add("none");
            ignoring.Add("noone");
            ignoring.Add("nor");
            ignoring.Add("not");
            ignoring.Add("nothing");
            ignoring.Add("now");
            ignoring.Add("nowhere");
            ignoring.Add("of");
            ignoring.Add("off");
            ignoring.Add("often");
            ignoring.Add("on");
            ignoring.Add("once");
            ignoring.Add("one");
            ignoring.Add("only");
            ignoring.Add("or");
            ignoring.Add("other");
            ignoring.Add("others");
            ignoring.Add("otherwise");
            ignoring.Add("ought");
            ignoring.Add("our");
            ignoring.Add("ours");
            ignoring.Add("ourselves");
            ignoring.Add("out");
            ignoring.Add("outside");
            ignoring.Add("over");
            ignoring.Add("own");
            ignoring.Add("part");
            ignoring.Add("per");
            ignoring.Add("perhaps");
            ignoring.Add("please");
            ignoring.Add("put");
            ignoring.Add("rather");
            ignoring.Add("re");
            ignoring.Add("same");
            ignoring.Add("see");
            ignoring.Add("seem");
            ignoring.Add("seemed");
            ignoring.Add("seeming");
            ignoring.Add("seems");
            ignoring.Add("several");
            ignoring.Add("she");
            ignoring.Add("should");
            ignoring.Add("show");
            ignoring.Add("side");
            ignoring.Add("since");
            ignoring.Add("so");
            ignoring.Add("some");
            ignoring.Add("somehow");
            ignoring.Add("someone");
            ignoring.Add("something");
            ignoring.Add("sometime");
            ignoring.Add("sometimes");
            ignoring.Add("somewhere");
            ignoring.Add("still");
            ignoring.Add("such");
            ignoring.Add("take");
            ignoring.Add("than");
            ignoring.Add("that");
            ignoring.Add("the");
            ignoring.Add("their");
            ignoring.Add("theirs");
            ignoring.Add("them");
            ignoring.Add("themselves");
            ignoring.Add("then");
            ignoring.Add("thence");
            ignoring.Add("there");
            ignoring.Add("thereafter");
            ignoring.Add("thereby");
            ignoring.Add("therefore");
            ignoring.Add("therein");
            ignoring.Add("thereupon");
            ignoring.Add("these");
            ignoring.Add("they");
            ignoring.Add("this");
            ignoring.Add("those");
            ignoring.Add("though");
            ignoring.Add("through");
            ignoring.Add("throughout");
            ignoring.Add("thru");
            ignoring.Add("thus");
            ignoring.Add("to");
            ignoring.Add("together");
            ignoring.Add("too");
            ignoring.Add("toward");
            ignoring.Add("towards");
            ignoring.Add("under");
            ignoring.Add("unless");
            ignoring.Add("until");
            ignoring.Add("up");
            ignoring.Add("upon");
            ignoring.Add("us");
            ignoring.Add("used");
            ignoring.Add("very");
            ignoring.Add("via");
            ignoring.Add("was");
            ignoring.Add("we");
            ignoring.Add("well");
            ignoring.Add("were");
            ignoring.Add("what");
            ignoring.Add("whatever");
            ignoring.Add("when");
            ignoring.Add("whence");
            ignoring.Add("whenever");
            ignoring.Add("where");
            ignoring.Add("whereafter");
            ignoring.Add("whereas");
            ignoring.Add("whereby");
            ignoring.Add("wherein");
            ignoring.Add("whereupon");
            ignoring.Add("wherever");
            ignoring.Add("whether");
            ignoring.Add("which");
            ignoring.Add("while");
            ignoring.Add("whither");
            ignoring.Add("who");
            ignoring.Add("whoever");
            ignoring.Add("whole");
            ignoring.Add("whom");
            ignoring.Add("whose");
            ignoring.Add("why");
            ignoring.Add("will");
            ignoring.Add("with");
            ignoring.Add("within");
            ignoring.Add("without");
            ignoring.Add("would");
            ignoring.Add("yes");
            ignoring.Add("yet");
            ignoring.Add("hey");
            ignoring.Add("you");
            ignoring.Add("your");
            ignoring.Add("yours");
            ignoring.Add("yourself");
            ignoring.Add("yourselves");
        }

        public void answers(ArrayList add_answers)
        {
            add_answers.Clear();

            // HELP - What can I ask about
            add_answers.Add("help|You can ask me about: passwords, phishing, privacy, scams, 2FA, safe browsing, and cybersecurity.");
            add_answers.Add("what can I ask|You can ask me about: passwords, phishing, privacy, scams, 2FA, and safe browsing.");

            // PASSWORD
            add_answers.Add("password|A password is used to secure access to your accounts. Use strong, unique passwords for each account.");
            add_answers.Add("password|Create passwords with at least 12 characters mixing uppercase, lowercase, numbers, and symbols.");
            add_answers.Add("password|Never use personal information like birthdays or pet names in your passwords.");

            // PHISHING
            add_answers.Add("phishing|Phishing is a scam where attackers pretend to be trusted sources to steal your information.");
            add_answers.Add("phishing|Always check the sender's email address carefully before clicking any links.");
            add_answers.Add("phishing|Be suspicious of urgent language like 'Your account will be closed' or 'Immediate action required'.");
            add_answers.Add("phishing|Never download attachments from unknown senders - they could contain malware.");

            // PRIVACY
            add_answers.Add("privacy|Review your privacy settings on social media regularly to control who sees your information.");
            add_answers.Add("privacy|Use a VPN when connecting to public Wi-Fi to encrypt your internet traffic.");
            add_answers.Add("privacy|Be careful what personal information you share online - it can be used by scammers.");

            // SCAM
            add_answers.Add("scam|If an offer sounds too good to be true, it probably is a scam!");
            add_answers.Add("scam|Never send money or gift cards to someone you haven't met in person.");
            add_answers.Add("scam|Verify charity organizations before donating - scammers create fake charities.");

            // 2FA
            add_answers.Add("2fa|Two-Factor Authentication adds an extra layer of security to your login process.");
            add_answers.Add("2fa|Use authenticator apps like Google Authenticator instead of SMS when possible.");
            add_answers.Add("2fa|Save your backup codes in a safe place in case you lose access to your phone.");

            // BROWSING
            add_answers.Add("browsing|Look for HTTPS in the URL before entering sensitive information on any website.");
            add_answers.Add("browsing|Keep your browser and extensions updated - updates include critical security patches.");
            add_answers.Add("browsing|Avoid using public computers for banking or shopping online.");

            // CYBERSECURITY
            add_answers.Add("cybersecurity|Cybersecurity is about protecting your digital information from online threats.");
            add_answers.Add("cybersecurity|Good cybersecurity habits include using strong passwords and being cautious online.");

            // GREETINGS
            add_answers.Add("hello|Hello! How can I help you with cybersecurity today?");
            add_answers.Add("hi|Hi there! Ask me about passwords, phishing, privacy, or safe browsing.");
            add_answers.Add("hey|Hey! What would you like to learn about online safety?");

            // HOW ARE YOU
            add_answers.Add("how are you|I'm doing well, thanks for asking! How can I help you stay safe online today?");

            // PURPOSE
            add_answers.Add("purpose|My purpose is to educate you about online safety and help protect you from cyber threats.");

            // THANK YOU
            add_answers.Add("thank|You're welcome! Stay safe online!");
            add_answers.Add("thanks|Glad I could help! Feel free to ask more questions.");

            // GOODBYE
            add_answers.Add("bye|Goodbye! Remember to use strong passwords and stay vigilant online!");
            add_answers.Add("exit|Take care! Stay safe in the digital world!");
        }
    }
    
}