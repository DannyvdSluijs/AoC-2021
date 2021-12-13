using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021DotNet.AoC
{
    public class Day10
    {
        public void Part1()
        {
            var data = input.Trim()
                .Split("\n")
                .ToList();
            var mismatches = new List<char>();

            foreach (var line in data)
            {
                var stack = new List<char>();
                foreach (var x in line)
                {
                    if (IsChunkOpen(x))
                    {
                        stack.Add(x);
                        continue;
                    }

                    var top = stack.Last();
                    stack = stack.Take(stack.Count - 1).ToList();

                    if (!IsMatch(top, x))
                    {
                        mismatches.Add(x);
                        break;
                    }
                }
            }

            var values = mismatches.Select(x =>
            {
                return x switch
                {
                    ')' => 3,
                    ']' => 57,
                    '}' =>1197,
                    '>' => 25137,
                    _ => throw new ArgumentOutOfRangeException(nameof(x), x, null)
                };
            });

            Console.WriteLine($"{values.Sum()}");
        }

        public void Part2()
        {
            var data = input.Trim()
                .Split("\n")
                .ToList();
            var mismatches = new List<char>();
            var scores = new List<long>();

            foreach (var line in data)
            {
                var stack = new List<char>();
                var match = true;
                foreach (var x in line)
                {
                    if (IsChunkOpen(x))
                    {
                        stack.Add(x);
                        continue;
                    }

                    var top = stack.Last();
                    stack = stack.Take(stack.Count - 1).ToList();

                    if (!IsMatch(top, x))
                    {
                        mismatches.Add(x);
                        match = false;
                        break;
                    }
                }

                if (stack.Count > 0 && match)
                {
                    long totalScore = 0;
                    stack.Reverse();
                    foreach (var x in stack)
                    {
                        totalScore *= 5;
                        totalScore += x switch
                        {
                            '(' => 1,
                            '[' => 2,
                            '{' => 3,
                            '<' => 4,
                            _ => throw new ArgumentOutOfRangeException(nameof(x), x, null)
                        };
                    }

                    scores.Add(totalScore);
                }
            }

            scores = scores.OrderBy(s => s).ToList();
            var result = scores.Skip((scores.Count - 1) / 2).First();

            Console.WriteLine($"{result}");
        }


        private static bool IsChunkOpen(char c)
        {
            return c is '(' or '[' or '{' or '<';
        }

        private static bool IsMatch(char left, char right)
        {
            switch (left)
            {
                case '(' when right == ')':
                case '[' when right == ']':
                case '{' when right == '}':
                case '<' when right == '>':
                    return true;
                default:
                    return false;
            }
        }

        private string testData = @"
[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]
";

        private string input = @"
{{[([{{[<{{<[[()()]]((<>())[<>[]])>{{{[]<>}[<>]}<[{}{}][[]{}]>}}<<<<<>()>>((<>())<{}()>)><{{[]()}{()
{<<{({{[<<[<(<<>[]><<>>){<{}()>[{}()]}>]{{[{{}{}}([]())][{[]()>[[][]]]}<(<{}()><<>[]>)>}>>{[(
[[<{<{{{([[{[[{}()]<<>{}>]}{{<(){}>{<>}}{{<><>}{<><>}}}][[<<()[]>(<>())>[(())([]())]>]])<{
{<[([[<<<(<<<<(){}>>{([]<>){[]{}}}><<[()()][{}[]]>[(<><>)[<>]]>>){[<[<()<>>[[]()]]<<()[]>(<>{})>>[(([](
{[<(<<{{[[[[(<{}()>[(){}]){[()()]{<>{}}}]]]]{([(<<[]{}><<>[]>><[{}[]]{()[]}>)])([<({<>[]}({}<>))([()]({}[]))>
(<[{<[{([[(<<<{}<>){[]<>}>(([][])([]()))>[({()[]}<{}<>>)<{{}<>}{(){}}>])(([{{}()}<[]()>]<({}<>)(<>
{[<<((<{([({([()]<()()>)({<>{}}<[][]>)}<[{(){}}](<<>>)>){<[(<>{})]({(){}}<<>{}>)><{<{}()><[]()>}>}
[[([{<<(((((({<>[]}<<>()>))((({}{})<[][]>)({<>()})))[{[[()]{<>[]}]}[(<<><>>{[][]})({<>[]}[{}<>])]])[<{<
(<([[{{<((<<[(<>{})]<[<><>](()[])>>>(<<{[][]}([]()))>)))>}}]])[[[({[(((<([{}<>])[([]{})<()()>]>))(<<[(<><>)<[
{(<([<[[[<<{<<{}[]>>}[{(<><>)[{}<>]]{[[][]]<[]<>>}]>[<[<()[]>[{}()]]<([]<>){<>{}}>>[{{()[]}([])}[[[]]]]
({{<{(<(({[{{<<>()><()<>>}}]}){[{{(({}<>>)}}<{<<{}[]>>}>]})((<((((<><>)[(){}]){<(){}><{}()>}){({(
[(<{((<[{({<[<{}><[][]>]<{[]<>][[]()]>>[[[[]{}]](<{}<>><{}>)]}({{{[]()}[[][]]}[<{}[]>[{}[]]]}{(({}
(((<<[{{{{{{<[<>][<>[]]>)}}}<<[[[[[]{}]<[]{}>](<[]{}>)][(<<>{}>({}()))]]([[{{}{}}[{}<>]]{{[]
([<[<({(({<{[{()[]}][({}[]){<>{}}]}>{{{[[]{}][<>()]}}<(({}){()()})<[()<>][()]>>}}<([({<><>}({}))({()()}[
<[{<[{{<{(<{<(()[]){{}{}}><{[]{}}{{}()}>}<{[[][]][<><>]}<<()}({}())>>><([[[][]](()<>)])>)(<([<[]<>><<>[]>]{
<((({{<<([[<{({}<>)({}<>)}{[<><>][(){}]}>({[[]][()<>]}<([]<>){<><>}>)]][{{{{<>[]}[{}()]}[(()())<[][]>]}{
<([<[[(<(((({[{}[]][[]<>]}<([][])<[]{}>>){[(()())({}{})](<<>{}>)})))))[{[({[[{{}{}}({}())]][
([[(([{([[<<{[[]()]{[]{}}}{{<>()}}><[<()()>([]<>)]{<(){}>((){})}>>({(<()<>>{()[]})}[(<[][]><[]<>>)]>]{
(({{[([{({{([<()<>>{{}[]}]{({}<>)(()[])})}((<[[]]<{}[]>>{(()<>)[{}<>]})[(<()()>){{()[]}({}()]}])}{(([
[[[(<[<{{([[[({}[])]({(){}}{<>})]<[<{}>]>](([[<>()]{{}{}}]{<<>()>[<>[]]})((({}())<{}()>)<<[]{}>>))]}(<<(
{{([<[<<({({[[{}[]]<[]()>]<{<>{}}[[]<>]>}(([{}{}]{{}<>})[<[][]><[]{}>]))[[[{()[]}{<><>}]]]}<
{([<[<[{[(<{([()()]<[]<>>}<[()()]<()()>>}<{[[]()]<(){}>}([<>[]]{<>()})>>[[((<>[])(<><>))[{{}()}[<>[]]]]<
<{<{((({[[[<{<[]{}><()[]>}>([<<>[]><<>()>]<[<>()][[]<>]>)]]]}(<({<<[(){}]><{[]<>}[{}[]]>><{{[]{}}((){})
[{{{([{[([<[({[]{}})<({}<>){<>{})>][{{<>}<<>()>}<[<>{}]{()[]}>]>{{[[{}<>][<><>]]{<{}()><[][]>}
<<{<<([[([[(<(()<>){[]()}><{<>[]}<<>[]>>)]{<(<[]{}>((){}))([[][]]({}[]))>{[({}[])(<>)]({()()}<<>[]>)}}]
[[([{(([(<[({({}[])<{}[]>})(<(()())(()())>[{<>{}}({}[])])]>[(<<(<>{})>[<{}{}>[<><>]]><<[()()]
[[{{<<((<[{{<(<>[])(())>}}<{{<()<>>}}>]{{<{<[]>(()[])}(<()()><(){}>)>}(<{<{}[]>(<>())}[<()()>{<>()
<<({<<([({((({{}{}}<[]()>)([{}{}])))[<[<<>{}><{}[]>}{((){}){{}{}}}>(<((){})<{}[]>>([<><>](<>[])))]})[
{({<(([[<{{<[<()>][[[]{}]{<>{}}]>[[[()[]]({}<>)][{[][]}{[][]}]]}<<{[()[]][()()]}[({}{})(<>{})]>)}>{[[[{
(({[[<(<[((<(({}[])([]()))<(<>[])[()<>]>>))<[[[{{}[]}<()[]>}]<{{<>[]}{(){}}}>]>]{[{<<<{}<>
<{{<<[(([[([<{[][]}<<>()>><<<>>>])<{((<>{})[[][]])({{}<>}<[]{}>)}(<<{}{}><{}{}>><(<>[])({}<>)>)>]]{<<
{([[<<[({[<(<(()())><{[]()}<()[]>>)<<([][])(()[])>[<[]<>><<>()>]>><{([<>[]]{{}})<{{}[]}{<><>}>}[{{[]}{[]{}
<{[<<(<[[<{(<{{}[]}<[][]>>{[(){}]})([({}{})<{}()>]<<()><{}>>)}>({<{([][])}<{[]{}}<<>{}>>>{(<[]
{{<[[<([[[{[[{<>[]}<(){}>]([<>[]]({}<>])]{<[{}()]{{}[]}>[<{}()>(()[])]}}<{[((){})]}([({}<>){<><>
([<([{<(<[{[{{{}<>}[()<>]}<<()>{[]<>}>]}[[[<<><>><[]<>>]{<{}[]>{<>{}}}]]](<([<<><>><()()>][({}
{{{[{[{([([<[[{}<>]{{}[]}]{<<><>>{{}<>}}>][<<[[]{}][()()]>{{[]}{{}{}}}>{[<[][]>[{}<>]]{(()<>){[]
[<{{<[[[[<{<({(){}}[<><>]>({{}}<<><>>)><({{}<>}[[][]])(([][])(<>{}))>}>]<[(<{[{}()]{()()}}<((){})>
{<<{{<{<({[{{({}())<{}{}>}[[{}[]](<>[])]}<({[][]}<{}<>>)<<<>[]>[{}[]]>>]{{<{[]<>}<[]<>>>{<<>[]>}}}}<
{[(<({([([{[{[<>[]][()()]}<{{}[]}[{}{}]>]}([{{{}<>}([]())}<[<>()](()[])>]{[(()<>)(<><>)]})]<[([{[]
({((<<({{([[<{(){}}<()[]>>(<[]{}>{<>[]})]<{{{}()}(<>[])}[<()[]><{}()>]>]<<[[{}[]]{{}[]}]({()()}[{
{<{<(((<({[<{{(){}}[<><>]}<<{}{}><{}{}>>>]({<[[]{}](<>)>{[[][]]{(){}}}}[{(())<{}[]>}{<<>[]]{()
<<{<({((<[((<<()<>>[<>[]]>{{{}<>}({}<>)})([(()()){[]()}]))[[[<[]<>><<>>]{<<>()><()[]>}]]]>)[(<{(<{{}{}
[({<{{<<{<<{<[<>[]]<()<>>>[({}())([][])]}>>}>>}((([{{<([()[]]{{}()})<<(){}>(<><>)>><(<()()>(<>{}
{[{<<[(<<[(<<{<>[]}{[]{}}>[[[]{}](()())]>)(([(<>[])(<><>)](<[]<>>(()())))[(({}{})([]{}))])](<<({{}()}
<<((({((({<{{{{}{}}[[]()]}([()<>]{<>()})}>}){<{{<(<>())><<()()>[()()]>}}>{[{([()<>]<[]<>>)[<{}(
(([[[{{[{{<<(<{}>{{}()}){{[]()}{<><>}}><[({}[])<<>{}>](<<>{}>[{}[]])>>([<<[]{}>[()<>]>[([]()){(
<({<{<[({{(([{{}()}[[][]]]<{()<>}{<>()}>))<[[({}())({})][{<><>}<[]()>]]{[[<>]{[]{}}]}>}[({{<{}<>>}}
[({{[<[[[[({<{<>[]}{{}{}}>{{{}{}){()[]}}}(<<()<>>{[]{}}>))]({[{<[]()>(<>[])}[(<><>)]]})]]]{<[<(
<<<[({{{[{[(<{()}(()[])><([][])([][])>)({{{}}{<>{}}})]{<[([]<>)[{}()]]((<>())(<><>))>>}{<{{
({([<(([({<<<<[]()>(<>{})><<[]>{<><>}>>{<{<>}{{}()}>({<><>}{[]{}})}>}[<<{(()<>)[{}<>]}([()()]<()()>)>[
[<({{<{<[{{{(((){}))({{}()})}}({[[{}<>]<<>{}>](((){})[[]<>])})}(<<<<()()><[]<>>><<[]{}><()<>>>><<<()<>><<>()
{{(([<<{{({[<{[]}[<>[]]>(<<><>>[[]{}])]{{(<>())({}())}[<[]()>[()[]]]}}[(<[{}()](<>{})><<{}
{<({{([({<<[[[[]{}]{<>()}]{<(){}>{()()}}]>(<{{(){}}({}{})}<<{}()>>>{[<<>()>>{[<>{}]{<>{}}}})>}
{{([<<{<[<([{[{}[]]}(<<>{}>(<>{}))]<<{()()}[<>[]]>{<[]()>([]{})}>)({(<[]><[]>)[<[]<>><{}()>]}(
<((<(({({[(<{[<>[]][{}{}]}{{(){}}}>{{<<>[]>{[]}})){<{<<><>>}<<[]{}>{[][]}>>((<()<>>{()()})([[]()]([]{})))}][[
[<[(<(<{(<<{[[{}<>]]}[{{<>{}}{<><>}}[<[][]>{(){}}]]><{<({}<>){{}[]}>(({}[])({}[])>}[{{{}{}}
<((({{[([<<(<(<>())({}())>({()<>}))({<[][]>([]{})}({(){}}[{}{})))>>])]{<[{[<({(){}}{<>})>{{[{}()](
(<<([<<(<[({(<{}()>[<>[]])[({}{}){<>[]}]}(({()[]}<()()>)([[]()]<(){}>)))(<{{<>{}}[{}[]]}([<>{}][(
<({<(([[((([<<()[]>[<>{}]>](<[(){}]<[]{}>)<[[]][[]<>]>))(([<<><>>{[]{}}]))))[({({<[]()><{}()>}<[
{[{{([(({[{[<[[][]]((){})>[[<><>]([][])]]{([[]{}][<>()]){(()[])[[]<>]}}}[<({{}[]}{<>()})[([]())<{}()>
(<(<[<(<({[[((()()){<><>))[(<>())(()())]][[<<>{}>([]())][{<>()}[<>]]]]}[([{[[]<>]}{<[]{}>}]){<<([]{}){{}[]}>>
[([{(<{[({(([<[][]>[{}<>]]<(<><>)>){({()[]}{()[]})({()}(<>[]))})<[[([]())[(){}]]{[()<>]<[]<>>}]((<<>(
<(<{([{<[{<<[[<>[]]]><<[<>{}][{}{}]>>>([({[]()}<<>()>)[<[]()>]]{<{()<>}>[{[]()}{<>[]}]})}({{[(()[])<[]()
({{<{{[<({{([<()[]><{}<>>]<{[][]}[<><>)>)[<(<>{}){{}()}>(<<>{}>[{}[]])]}<{((()<>)[()()])<((){}){{}{}}>
<{[[{{{[(([<({<><>}{<>()})>({<[]{}>[<><>]})]){<[[[{}[]]{()()}]{[<><>)[{}{}]}]><((([][])){[[]
{[{[[[{[<<{{[[[][]]{{}()}][<<>()>{(){}}}}{{<<>{}>[{}[]]}}}[({<<>[]>{[]{}}}<({}<>)[[]()]>)]>({(<<[]<>>
<[[{(<({<({<([()<>])<{<>[]}{{}()}>>})<([<(<>[])<()()>>(<(){}>[{}[]])>([[{}[]]({})]{{<><>}<<>[]>}))[[{[{}
<{{[[{<(<[((((()<>)<{}[]>)<((){})[(){}]>))][[<[({}<>){()}]<[<>{}]{[][]}>>({[()()][()()]})]]>)>}]<<<(
[<[<{(<{<[(([<[][]>])[((<>()>([][]))[{()}{{}<>}]])({[[{}<>](()())](({}())<()[]>)}<{(<>())[(){}]
[<<[[(([[[({[{()[]}(<>{})]{[{}]<<>()>}}{[{[]<>}{()()}]<([][])>})][{((<<>()>(<>[])){{<><>}<()[]>})<[[{}(
<{<([[[([[{[{{[]<>}<{}<>>}](<{()[]}(<>)>{<{}{}>([]{})})}]<<{{{<>{}}{<>()}}<<<>()>[()[]]>}[{[[]
{[[[([{<([(([[()[]]([][])][[<>()](()<>)]]<<{(){}}[(){}]>[{[]()}(<>)]>)<[[[{}()][()]]<({}{}){<>()}>]([[()()]{
(({<(({[(<(({({}[])({}[]]}([[]()]([]())))[<<()<>>{{}()}>[[()()]]])>)<([[[[{}[]]{<>()}]]({([]())(<>())
<[<{({{<{({<{[{}()]{[]}}<[()<>]<(){}>>>[[<[]{}>{{}<>}]<[[]]<<>[]>>]}<{<[<>()][{}{}]>{[()()][<>]}}>)}{{{[[[[]<
(({{([([{{{[{{{}[]}{<><>}}<{{}}(<>())>][(([]{}){<><>})<<[]<>>[<>()]>]}}{{[([<>{}]<()()>)({(){}})]({(<
<<<<(([(([{[<[(){}]([]<>)>]((([][])({}{}))(([]{})<[]>))}])(({{[{{}()}<[]<>>](({}<>)[<>()])}({{[
{{[<[<[([{(({([]())(<>())}[[[]<>]([]{})])[{<[][]>({}{})}{({}[]){()<>}}])}][{{(<(<><>)([]<>)>([<>()](()())))
[[[(<[<([[{[<<(){}>((){})>{<{}<>>({}())}][<<()>([]<>)>([<>[]]<[]>)]}{{<[<>()]<{}<>>><<()<>>{{}<>}>}<<[[]
[<<(<<({<{{(((()())))<[{()()}(()[])][[()()]]>}[{{(()<>)[[]()]}<<<>{}>{<><>}>}[([()[]]<()()>)(
<<{{{<<{{{<[{<[]()>([][])}<{<>{}}{[]{}}>]([{<>()}({}<>)])>}<<[([{}{}][{}])[{{}<>}(()<>)]][<({}())({}()
[<<<{[{(([<<[{<><>}[[]<>]][([][]){[][]}]>[[{()[]}[{}[]]]({()[]}[{}{}]>]>{<[<[]()><<>{}>]<{()[]}([][])>>}]<{
{{([({<[({{{<{[]<>}{<><>}>[<{}{}>{{}()}]}<<<[]()><<>()>}{(()[])[{}{}]}>}}<<([[[]()]{[]<>}]<(()[])([]{})>)[<((
[{<{{<[[<[{<(([]{})<()<>>)(<[][]><[]()])>[{({}<>){{}()}}({()[]}[(){}])]}]>]]{[<({<{<<><>>}<{(
[[<[(<{[<({[(<{}{}>(()()))]<[<[]<>>[{}()]]<({}())([]())>>})>[([[(({}{}){[][]}){{{}<>}({}{})}]]({<[{}[]][()
([(<[<<(<{((<{()[]}([]{})>(<(){}>)){(({}<>){()[]})<[<>[]]<(){}>>}}}>[{{{[[{}[]]]}((<<>()><{}{}>
({{{[(<({[<{<({}<>){()[]}>[<()<>>(<>{})]}<[<{}()>([]<>)]((<>{})[{}[]])>>({[{{}<>}(<><>)}}{({{}()}<<><>>){[
([[<<{<<[<{(<({}<>)<[]<>>>)[[[{}[]][{}<>]]([()[]](<>()>)]}>][{{[[<<><>>][[{}()]<{}[]>]]{<{()[]}{{
{(<<[(<<({[{<[[]()){(){}}>{[[]{}]<{}{}>}}<{<()>[()()]}[<()()><<><>>]>]<<[<[][]><<>{}>]{{[]
[<({[({{{<[<({[]{}}[[][]])>]>[{{{{<>{}}({}()}}(<[][]>{[]()})}([<<><>><{}{}>])}{[<<{}{}>(()[])><{<
{([[<[({{<(({(()<>){<>{}}}{[<>[]](<>[])}))<[<[<><>]>[([][]){{}()}]]{<(<>())>}>>}[({[{<<>{}>({}[])}<{[]{}
(<<[[[[[<<(({([]()){<>()}}{[{}<>]<<>()>})[{(<>{}){[]()}}]]<<[<[]()>[[][]]]>[<[[]<>]([]{})>{
{[[([({<[{<(({{}()}([]())){{[]()}[(){}]}){[<[]<>>[[]<>]]<(()){<>{}}>}>({((()())({}<>))([()()](
[({<{<{[[{{<<{[]}>{<{}[]>[{}()]}>([<[]()>(())])}}<(((({}())(<><>))))(<[{(){}}([]())][[{}[]][()[]]]>[{{{}()}
[[((([[{<(<<<{{}{}}{()()}>{[[]()]{(){}}}><<(<>())>([<>{}]{<>{}})>>)>[{({{([]{}){<>[]}}{(()
{([{([<({[[[([{}{}]<<>()>)((()())<[]<>>)][<[(){}]<[]()>>[<<><>>]]]<<<({}<>)[()()])<<[]<>><()()
{<(<<{({{([<{[()<>]{()<>}}<[(){}]<{}[]>>>](([<[]>([]())]{([])((){})})((([][])(()<>})[{{}}<(){}>])))}<<((<
([{<{{<{[{<{<{<><>}{{}()}><<[]()>(()[])>}{<(<>())[<>[]]>{{[][]}<()()>}}><<{(()[]){{}()}}>>)(({[[{}
{{(<({<[<([{([()<>]([]()))<<[]{}>[[]{}]]}{<({}<>)(<>[])><<[]{}>[[]()]>}][{{((){})<{}[]>}<(<><>
<({<{[[{[({(<[()[]]{[][]}>{<[]()><()()>>)<{<(){}>((){})}(([]{})({}{}))>}[<({[]<>}([]<>))<({}){<>[]}
([[{(((({{{[<<{}{}>{()()}>({[]<>}[[]()])][{[{}<>]<()()>}([<><>]))}}{({(<[][]>[<><>])}[{[<>()]<[]{}
([{{<[(<([{{(([]())({}[]))}{{(<>())[<>{}]}<(()[])([]())>}}{<{<()>{{}{}}}>(<[{}]<{}{}>><{{}{}}([]{})>)}]){<
{((<{[<<(<([(<{}<>>)]<(<[]{}>{[][]}}[[()()][[]<>]]>){{{[[]{}][<>()]}<((){})(<>())>}{(([]{})<[][]>)[([]())
{({[<{<{[{[<{([]<>)({}<>)}<({}())({}[])>>{{{<>{}}([][])}[<[][]>]}]}((([<<>{}>{<>[]}][<{}{}><()()>])[
{({{(({(<<<[(<<><>><<>[]>)((())<{}<>>)]>{<[<()<>><[]>]{{<><>}[{}[]]}>}>><{[{([{}{}]{<><>})[{
(<{[[({(<{[[(<[]()><<><>})<([]()){[][]}>]{((<>{})[()<>])}]}{[{{<[]<>>{[][]}}[<[]<>><<><>>]}]{{{{
<<(({[[{[<((<[{}{}]{[]()}>{{()[]}<<><>>})([[(){}](<>[])]<<{}[]>[(){}]>))<<{{{}[]}({}{})}(<()<>>[
";
    }
}